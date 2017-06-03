using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System;
using TestStack.BDDfy;

namespace ReadOne.Application
{
    public abstract class BaseBook
    {
        protected void BookExists()
        {
            throw new NotImplementedException();
        }
        protected void BookNotExists()
        {
            throw new NotImplementedException();
        }
        protected void BookIsNotStartedByYou()
        {
            throw new NotImplementedException();
        }
        protected void BookIsNotStartedByAnyUser()
        {
            throw new NotImplementedException();
        }
        protected void BookIsStartedByAnyUser()
        {
            throw new NotImplementedException();
        }
        protected void BookIsAlreadyStartedByYou()
        {
            throw new NotImplementedException();
        }
    }
}
