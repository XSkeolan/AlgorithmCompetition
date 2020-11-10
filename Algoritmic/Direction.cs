using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algoritmic
{
    /// <summary>
    /// Представляет собой направления для передвижения
    /// </summary>
    public enum Direction
    {
        LeftTop = 2,
        LeftButtom = 3,
        RightTop = 1,
        /// <summary>
        /// Движение в сторону 4 четверти координатной оси
        /// Используется только для выполнения двух действий подряд
        /// Первое действие - передвижение вправо
        /// </summary>
        /// <remarks></remarks>
        RightButtom = 4,
        /// <summary>
        /// Движение вверх по оси Y
        /// </summary>
        Top,
        /// <summary>
        /// Движение влево по оси X
        /// </summary>
        Left,
        /// <summary>
        /// Движение вправо по оси X
        /// </summary>
        Right,
        /// <summary>
        /// Движение вниз по оси Y
        /// </summary>
        Buttom
    }
}