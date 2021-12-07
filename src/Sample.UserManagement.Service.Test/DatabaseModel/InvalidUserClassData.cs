using Sample.UserManagement.Service.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.UserManagement.Service.Test.DatabaseModel
{
    public class InvalidUserClassData : IEnumerable<object[]>
    {
        private static Guid _id = Guid.NewGuid();

        private readonly List<object[]> _users = new List<object[]>
        {
            new object[]
            {
                new UserDbModel(){
                    Id = Guid.NewGuid(),
                    FirstName = "Jacob",
                    LastName = "Jalali",
                    Email = ""
                },
                new UserDbModel(){
                    Id = Guid.NewGuid(),
                    FirstName = "Jacob",
                    LastName = "Jalali",
                    Email = ""
                }
            },
            new object[]
            {
                new UserDbModel(){
                    Id = _id,
                    FirstName = "Jacob",
                    LastName = "Jalali",
                    Email = ""
                },
                new UserDbModel(){
                    Id = _id,
                    FirstName = "Jacob",
                    LastName = "     ",
                    Email = ""
                }
            },
             new object[]
            {
                new UserDbModel(){
                    Id = _id,
                    FirstName = "Jacob",
                    LastName = "Jalali",
                    Email = ""
                },
                new UserDbModel(){
                    Id = _id,
                    FirstName = null,
                    LastName = "Jalali",
                    Email = ""
                }
            },
             new object[]
            {
                new UserDbModel(){
                    Id = _id,
                    FirstName = "Jacob",
                    LastName = "Jalali",
                    Email = ""
                },
                new UserDbModel(){
                    Id = _id,
                    FirstName = "Jacob",
                    LastName = "Jalali     ",
                    Email = ""
                }
            },
        };

        public IEnumerator<object[]> GetEnumerator()
        { 
            return _users.GetEnumerator(); 
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        { 
            return GetEnumerator(); 
        }
    }
}
