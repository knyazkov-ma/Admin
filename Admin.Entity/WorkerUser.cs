namespace Admin.Entity
{
    /// <summary>
    /// Пользователь исполнитель/диспетчер
    /// </summary>
    public class WorkerUser : BaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// E-mail (он же логин)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Тип пользователя
        /// </summary>
        public TypeWorkerUser UserType { get; set; }

        /// <summary>
        /// Заблокирован
        /// </summary>
        public bool Disabled { get; set; }
    }
}
