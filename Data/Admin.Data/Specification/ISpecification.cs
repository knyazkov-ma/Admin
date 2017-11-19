﻿using Admin.Entity;
using System;
using System.Linq.Expressions;

namespace Admin.Data.Specification
{
    
    public interface ISpecification<T> where T : BaseEntity
    {
        Expression<Func<T, bool>> IsSatisfied();
        ISpecification<T> And(ISpecification<T> specification);
        ISpecification<T> Or(ISpecification<T> specification);
    }
}
