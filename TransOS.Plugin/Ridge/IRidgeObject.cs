using System;
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
    public interface IRidgeObject
    {
        /// <summary>
        /// Object ID
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Parent object
        /// </summary>
        IRidgeObject Parent { get; set; }

        /// <summary>
        /// Child (sub) objects
        /// </summary>
        RidgeList Child { get; }

        /// <summary>
        /// Is the descendant list dynamically generated?
        /// (There is always the possibility of the presence of descendants)
        /// </summary>
        bool DynamicChilds { get; }

        /// <summary>
        /// This method is called before searching for descendants (expanding the TreeView)
        /// </summary>
        void Expanding();

        /// <summary>
        /// Получить объект определённого типа
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /*T GetObject<T>();

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
        Image Icon { get; }

        /// <summary>
        /// Main text
        /// </summary>
        string Text { get; }

        #endregion


        #region VisualContent

        /// <summary>
        /// Control for display
        /// </summary>
        Control View { get; }

        #endregion


        #region VisualMenu

        /// <summary>
        /// Is this menu item the default
        /// </summary>
        bool Default { get; }

        /// <summary>
        /// Whether the item is selected. Attention?
        /// </summary>
        //bool Selected { get; set; }

        //IRidgeList LeftMenu { get; }

        RidgeList RightMenu { get; }

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
        void Run(params object[]args);

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
    }
}
