namespace MovieHub.Services.Interfaces
{
    using Models;
    using System;
    using System.Collections.Generic;

    public interface IDirectorService
    {
        void InsertDirectors(ICollection<Director> directors);
    }
}
