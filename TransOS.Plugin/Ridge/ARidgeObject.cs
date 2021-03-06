﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TransOS.Plugin.Ridge
{
    /// <summary>
    /// Ridge object interface
    /// </summary>
    public abstract class ARidgeObject : IRidgeObject
    {
        /// <summary>
        /// Object ID
        /// </summary>
        public string Id { get; protected set; }

        /// <summary>
        /// Parent object
        /// </summary>
        public IRidgeObject Parent { get; set; }

        /// <summary>
        /// Child (sub) objects
        /// </summary>
        public RidgeList Child { get; protected set; }

        /// <summary>
        /// Is the descendant list dynamically generated?
        /// (There is always the possibility of the presence of descendants)
        /// </summary>
        public bool DynamicChilds { get; protected set; } = false;

        /// <summary>
        /// This method is called before searching for descendants (expanding the TreeView)
        /// </summary>
        public virtual void Expanding()
        {

        }

        /*
        /// <summary>
        /// Получить объект определённого типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetObject<T>();

        /// <summary>
        /// Получить объект определённого типа
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetObject(Type type);

        /// <summary>
        /// Передать объект
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        bool SetObject(object ob);*/

        #region VisualItem

        /// <summary>
        /// Image icon
        /// </summary>
        public Image Icon { get; protected set; }

        protected string Text_ = null;

        /// <summary>
        /// Main text
        /// </summary>
        public virtual string Text
        {
            get => this.Text_;
            set
            {
                bool CanSet = false;
                if (string.IsNullOrWhiteSpace(this.Text_))
                    CanSet = true;
                else if (CanEditText)
                    CanSet = true;

                if (CanSet)
                {
                    this.Text_ = value;
                    this.TextEdited?.Invoke(this.Text_);
                }
            }
        }

        /// <summary>
        /// Can user manualy edit text
        /// </summary>
        public bool CanEditText { get; protected set; } = false;

        public event Action<string> TextEdited;

        public void BeginUserEdit()
        {
            if (CanEditText)
                this.BeginedUserEdit?.Invoke(this);
        }

        public event Action<IRidgeObject> BeginedUserEdit;

        public void BeginUserRemove()
        {
            if (CanEditText)
                this.BeginedUserRemove?.Invoke(this);
        }

        public event Action<IRidgeObject> BeginedUserRemove;

        public bool Remove()
        {
            if(this.Parent != null)
            {
                var r = this.Parent.Child.Remove(this);
                if (r)
                    this.Removed?.Invoke(this);
                return r;
            }
            return false;
        }

        public event Action<IRidgeObject> Removed;

        #endregion



        #region VisualContent

        /// <summary>
        /// Control for display
        /// </summary>
        public virtual Control View { get; protected set; }

        #endregion


        #region VisualMenu

        /// <summary>
        /// Is this menu item the default
        /// </summary>
        public bool Default { get; protected set; } = false;

        /// <summary>
        /// Whether the item is selected. Attention?
        /// </summary>
        //bool Selected { get; set; }

        // IRidgeList LeftMenu { get; }

        public virtual RidgeList RightMenu { get; protected set; }

        #endregion

        /*
        #region State

        /// <summary>
        /// Живой ли объект
        /// </summary>
        bool Live { get; }

        /// <summary>
        /// Задать живость объекта
        /// </summary>
        /// <param name="Live">Удалось ли изменить состояние?</param>
        /// <returns></returns>
        bool SetLive(bool Live);

        #endregion

        */

        #region Exec

        /// <summary>
        /// Run / execute the project. Click as menu item
        /// </summary>
        public virtual void Run(params object[] args)
        {

        }

        #endregion

        /*

        #region Cmd

        /// <summary>
        /// Воединиться с объектом
        /// </summary>
        /// <returns></returns>
        Stream Connect();

        /// <summary>
        /// Предложить соединение с объектом
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        bool Connect(Stream stream);

        #endregion

        /// <summary>
        /// Изменено состояние/параметр (визуально?)
        /// </summary>
        event Action Updated;

        */

        public ARidgeObject()
        {
            this.Child = new RidgeList(this);
        }

        public override string ToString()
        {
            return this.Text;
        }
    }
}
