﻿namespace Admin.DataService.DTO
{
    /// <summary>
    /// DTO для простых типов, чтобы не плодить однотипные классы
    /// </summary>
    public class SimpleDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

    }
}
