﻿using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.WebBrowser
{
    public partial class Form_Browser : Form
    {
        public Form_Browser(IDocument WebDocument, int ControlWidth)
        {
            InitializeComponent();

            var ГлавныйКонтрол = this.ПолучитьКонтрол(WebDocument.QuerySelector("body"), ControlWidth - 20, null);

            this.panel1.Controls.Add(ГлавныйКонтрол);
        }

        private Control ПолучитьКонтрол(INode Нод, int МаксимальнаяШирина, Uri АдресСтраницы)
        {
            Control новыйКонтрол = null;
            List<Control> Контролы;
            string СсылкаУрл, Текст;

            switch (Нод.NodeType)
            {
                case NodeType.Text:
                    Текст = Нод.TextContent.Trim(new char[] { '\r', '\n', '\t', ' ' });
                    if (!string.IsNullOrWhiteSpace(Текст))
                    {
                        новыйКонтрол = new TextBox
                        {
                            Location = new Point(0, 0),
                            Text = Текст,
                            ReadOnly = true,
                            Width = МаксимальнаяШирина,
                            BorderStyle = BorderStyle.FixedSingle
                        };
                    }
                    break;

                case NodeType.Element:
                    var ТегЭлемент = Нод as IElement;
                    switch (ТегЭлемент.NodeName)
                    {
                        // крнтейнеры
                        /*case "DIV":
                        case "BODY":
                        case "CENTER":
                        case "SPAN":
                        case "P":
                        case "STRONG":
                        case "UL":
                        case "LI":
                        case "LABEL":*/
                        default:
                            Контролы = new List<Control>();
                            foreach (var Поднод in Нод.ChildNodes)
                            {
                                var Полученный = this.ПолучитьКонтрол(Поднод, МаксимальнаяШирина - 2, АдресСтраницы);
                                if (Полученный != null)
                                    Контролы.Add(Полученный);
                            }

                            if (Контролы.Count != 0)
                            {
                                if (Контролы.Count == 1)
                                    новыйКонтрол = Контролы.First();
                                else
                                {
                                    новыйКонтрол = new Panel
                                    {
                                        Location = new Point(0, 0),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Width = МаксимальнаяШирина
                                    };

                                    Control ПоследнийКонтрол = null;
                                    foreach (Control Подконтрол in Контролы)
                                    {
                                        if (новыйКонтрол.Controls.Count > 0)
                                        {
                                            Подконтрол.Location = new Point(0, ПоследнийКонтрол.Location.Y + ПоследнийКонтрол.Height);
                                        }

                                        новыйКонтрол.Controls.Add(Подконтрол);
                                        ПоследнийКонтрол = Подконтрол;
                                    }

                                    новыйКонтрол.Height = ПоследнийКонтрол.Location.Y + ПоследнийКонтрол.Height;
                                }
                            }
                            break;
/*
                        case "A":
                            string ТекстСсылки = ТегЭлемент.InnerHtml
                                .Trim(new char[] { '\r', '\n', '\t', ' ' })
                                .Replace("\r", "")
                                .Replace("\n", "");

                            // ссылка - изображение
                            var img = ТегЭлемент.GetElementsByTagName("IMG").FirstOrDefault();
                            if (img != null)
                            {
                                СсылкаУрл = img.Attributes["src"]?.Value;
                                if (СсылкаУрл != null)
                                {
                                    // gif(анимацию) не грузим!!!
                                    string ex = Path.GetExtension(СсылкаУрл).ToLower();
                                    if (ex == ".gif")
                                        break;

                                    // нормализируем ссылку
                                    if (СсылкаУрл[0] == '/')
                                    {
                                        СсылкаУрл = Метаинструмент.Сеть.Хелпер.ПолучитьАдрес_Origin(АдресСтраницы) + СсылкаУрл;
                                    }

                                    // получаем файл
                                    var параметры = new DownloadParameters
                                    {
                                        Адрес = new Uri(СсылкаУрл),
                                        Кеш = CashOrder.СКешем,
                                        Завершено = this.СкачиваниеРисункаЗавершено,
                                        Refer = АдресСтраницы
                                    };

                                    this.mre.Reset();
                                    this.Ось.Network.Web.Клиент.СкачатьФайл_Начать(параметры);
                                    this.mre.WaitOne();

                                    // получаем ссылку
                                    СсылкаУрл = ТегЭлемент.Attributes["href"]?.Value.Trim();

                                    // -размер
                                    var Размер = this.СкачаннаяКартинка.Size;
                                    if (ТегЭлемент.Attributes["width"] != null)
                                    {
                                        int WidthAttribute = int.Parse(ТегЭлемент.Attributes["width"].Value);
                                        if (WidthAttribute > 0)
                                            Размер.Width = WidthAttribute;
                                    }
                                    if (ТегЭлемент.Attributes["height"] != null)
                                    {
                                        int HeightAttribute = int.Parse(ТегЭлемент.Attributes["height"].Value);
                                        if (HeightAttribute > 0)
                                            Размер.Height = HeightAttribute;
                                    }

                                    // создаём контрол
                                    var КартинкаКонтрол = new PictureBox
                                    {
                                        Size = Размер,
                                        Image = this.СкачаннаяКартинка,
                                        Cursor = Cursors.Hand,
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Tag = СсылкаУрл
                                    };

                                    Текст = ТегЭлемент.Attributes["title"]?.Value;
                                    if (!string.IsNullOrWhiteSpace(Текст))
                                        this.ГлавнаяФорма.toolTip1.SetToolTip(КартинкаКонтрол, Текст);

                                    новыйКонтрол = КартинкаКонтрол;
                                    новыйКонтрол.MouseEnter += ВизуальнаяСсылка_MouseEnter;
                                    новыйКонтрол.MouseLeave += ВизуальнаяСсылка_MouseLeave;
                                    новыйКонтрол.Click += ВизуальнаяСсылка_Click;
                                }
                            }

                            // текствовая ссылка
                            if (новыйКонтрол == null && !string.IsNullOrWhiteSpace(ТекстСсылки))
                            {
                                СсылкаУрл = ТегЭлемент.Attributes["href"]?.Value.Trim();
                                Color ЦветСсылки = Color.Blue;


                                var Ссыль = new LinkLabel
                                {
                                    Location = new Point(0, 0),
                                    Text = ТекстСсылки,
                                    AutoSize = false,

                                    Height = 20,
                                    Width = МаксимальнаяШирина,

                                    Tag = СсылкаУрл
                                };

                                if (СсылкаУрл == "#")
                                    Ссыль.LinkColor = Color.DarkOrange;

                                новыйКонтрол = Ссыль;

                                if (ТекстСсылки.Length > 6 && ТекстСсылки.Substring(0, 3) == "<b>" && ТекстСсылки.Substring(ТекстСсылки.Length - 4, 4) == "</b>")
                                {
                                    новыйКонтрол.Text = ТекстСсылки.Substring(3, ТекстСсылки.Length - 7);
                                    новыйКонтрол.Font = new Font(новыйКонтрол.Font, FontStyle.Bold);
                                }

                                новыйКонтрол.MouseEnter += ВизуальнаяСсылка_MouseEnter;
                                новыйКонтрол.MouseLeave += ВизуальнаяСсылка_MouseLeave;
                                новыйКонтрол.Click += ВизуальнаяСсылка_Click;
                            }
                            break;

                        case "IMG":
                            СсылкаУрл = ТегЭлемент.Attributes["src"]?.Value;
                            if (СсылкаУрл != null)
                            {
                                // gif(анимацию) не грузим!!!
                                string ex = Path.GetExtension(СсылкаУрл).ToLower();
                                if (ex == ".gif")
                                    break;

                                // нормализируем ссылку
                                if (СсылкаУрл[0] == '/')
                                {
                                    СсылкаУрл = Метаинструмент.Сеть.Хелпер.ПолучитьАдрес_Origin(АдресСтраницы) + СсылкаУрл;
                                }

                                // получаем файл
                                var параметры = new DownloadParameters
                                {
                                    Адрес = new Uri(СсылкаУрл),
                                    Кеш = CashOrder.СКешем,
                                    Завершено = this.СкачиваниеРисункаЗавершено,
                                    Refer = АдресСтраницы
                                };

                                this.mre.Reset();
                                this.Ось.Network.Web.Клиент.СкачатьФайл_Начать(параметры);
                                this.mre.WaitOne();

                                // создаём контрол
                                Текст = ТегЭлемент.Attributes["title"]?.Value;

                                // -размер
                                var Размер = this.СкачаннаяКартинка.Size;
                                if (ТегЭлемент.Attributes["width"] != null)
                                {
                                    int WidthAttribute = int.Parse(ТегЭлемент.Attributes["width"].Value);
                                    if (WidthAttribute > 0)
                                        Размер.Width = WidthAttribute;
                                }
                                if (ТегЭлемент.Attributes["height"] != null)
                                {
                                    int HeightAttribute = int.Parse(ТегЭлемент.Attributes["height"].Value);
                                    if (HeightAttribute > 0)
                                        Размер.Height = HeightAttribute;
                                }

                                var КартинкаКонтрол = new PictureBox
                                {
                                    Size = Размер,
                                    Image = this.СкачаннаяКартинка,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Tag = new Tag_PictureBox
                                    {
                                        IsLink = false,
                                        Title = Текст
                                    }
                                };

                                if (!string.IsNullOrWhiteSpace(Текст))
                                    this.ГлавнаяФорма.toolTip1.SetToolTip(КартинкаКонтрол, Текст);
                                новыйКонтрол = КартинкаКонтрол;
                            }
                            break;

                        case "H1":
                        case "H2":
                        case "H3":
                        case "H4":
                        case "H5":
                        case "H6":
                            Текст = Нод.TextContent.Trim(new char[] { '\r', '\n', '\t', ' ' });
                            if (!string.IsNullOrEmpty(Текст))
                            {
                                новыйКонтрол = new TextBox
                                {
                                    Location = new Point(0, 0),
                                    Text = $"[{ТегЭлемент.NodeName}]: {Текст}",
                                    ReadOnly = true,
                                    Width = МаксимальнаяШирина,
                                    BorderStyle = BorderStyle.FixedSingle
                                };

                                новыйКонтрол.Font = new Font(новыйКонтрол.Font, FontStyle.Bold);
                                новыйКонтрол.Height *= 2;
                            }
                            break;

                        case "SCRIPT":
                        case "LINK":
                        case "STYLE":
                        case "NOSCRIPT":
                        case "BR":
                            // Отключены
                            break;

                        case "FORM":
                            Контролы = new List<Control>();
                            foreach (var Поднод in Нод.ChildNodes)
                            {
                                var Полученный = this.ПолучитьКонтрол(Поднод, МаксимальнаяШирина - 2, АдресСтраницы);
                                if (Полученный != null)
                                    Контролы.Add(Полученный);
                            }

                            if (Контролы.Count != 0)
                            {
                                if (Контролы.Count == 1)
                                    новыйКонтрол = Контролы.First();
                                else
                                {
                                    новыйКонтрол = new Panel
                                    {
                                        Location = new Point(0, 0),
                                        BorderStyle = BorderStyle.FixedSingle,
                                        Width = МаксимальнаяШирина
                                    };

                                    Control ПоследнийКонтрол = null;
                                    foreach (Control Подконтрол in Контролы)
                                    {
                                        if (новыйКонтрол.Controls.Count > 0)
                                        {
                                            Подконтрол.Location = new Point(0, ПоследнийКонтрол.Location.Y + ПоследнийКонтрол.Height);
                                        }

                                        новыйКонтрол.Controls.Add(Подконтрол);
                                        ПоследнийКонтрол = Подконтрол;
                                    }

                                    новыйКонтрол.Height = ПоследнийКонтрол.Location.Y + ПоследнийКонтрол.Height;
                                }
                            }
                            break;

                        case "INPUT":

                            string InputType = ТегЭлемент.Attributes["type"]?.Value;
                            switch (InputType)
                            {
                                case "text":
                                    новыйКонтрол = new TextBox
                                    {
                                        Location = new Point(0, 0),
                                        Text = ТегЭлемент.Attributes["value"]?.Value,
                                        Width = МаксимальнаяШирина,
                                        //BorderStyle = BorderStyle.FixedSingle
                                    };
                                    break;

                                case "image":
                                    СсылкаУрл = ТегЭлемент.Attributes["src"]?.Value;
                                    if (СсылкаУрл != null)
                                    {
                                        // нормализируем ссылку
                                        if (СсылкаУрл[0] == '/')
                                        {
                                            СсылкаУрл = Метаинструмент.Сеть.Хелпер.ПолучитьАдрес_Origin(АдресСтраницы) + СсылкаУрл;
                                        }

                                        // получаем файл
                                        var параметры = new DownloadParameters
                                        {
                                            Адрес = new Uri(СсылкаУрл),
                                            Кеш = CashOrder.СКешем,
                                            Завершено = this.СкачиваниеРисункаЗавершено,
                                            Refer = АдресСтраницы
                                        };

                                        this.mre.Reset();
                                        this.Ось.Network.Web.Клиент.СкачатьФайл_Начать(параметры);
                                        this.mre.WaitOne();

                                        // создаём контрол
                                        Текст = ТегЭлемент.Attributes["title"]?.Value;

                                        // -размер
                                        var Размер = this.СкачаннаяКартинка.Size;
                                        /*if (ТегЭлемент.Attributes["width"] != null)
                                            Размер.Width = int.Parse(ТегЭлемент.Attributes["width"].Value);
                                        if (ТегЭлемент.Attributes["height"] != null)
                                            Размер.Height = int.Parse(ТегЭлемент.Attributes["height"].Value);*/
                    /*
                                        var КартинкаКонтрол = new PictureBox
                                        {
                                            Size = Размер,
                                            Image = this.СкачаннаяКартинка,
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Tag = new Tag_PictureBox
                                            {
                                                IsLink = false,
                                                Title = Текст
                                            }
                                        };

                                        if (!string.IsNullOrWhiteSpace(Текст))
                                            this.ГлавнаяФорма.toolTip1.SetToolTip(КартинкаКонтрол, Текст);
                                        новыйКонтрол = КартинкаКонтрол;
                                    }
                                    break;
                            }

                            break;

                        default:
                            новыйКонтрол = new TextBox
                            {
                                Location = new Point(0, 0),
                                Text = $"<<{ТегЭлемент.NodeName}>>",
                                ReadOnly = true,
                                Width = МаксимальнаяШирина,
                                BorderStyle = BorderStyle.FixedSingle
                            };
                            break;*/
                    }
                    break;
            }
            return новыйКонтрол;
        }
    }
}
