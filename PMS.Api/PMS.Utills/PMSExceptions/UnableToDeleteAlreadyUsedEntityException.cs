using System;
using System.Collections.Generic;
using System.Text;

namespace PMS.Utills.PMSExceptions
{
    public class UnableToDeleteAlreadyUsedEntityException : PMSException
    {
        public UnableToDeleteAlreadyUsedEntityException() : base("This entity is already in use, Hence deletion is restricted") { }

        public UnableToDeleteAlreadyUsedEntityException(string entityName) : base(string.Format("{0} is already in use, Hence deletion is restricted", entityName)) { }
    }
}
