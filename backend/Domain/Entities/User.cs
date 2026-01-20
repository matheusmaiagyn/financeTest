using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public class User
  {
    public Guid ID { get; private set; }
    public string Name { get; private set; }
    public int Age { get; private set; }

    // For EF Core
    protected User() { }

    private User(string name, int age)
    {
      SetName(name);
      SetAge(age);
    }

    public static User Create(string name, int age)
    {
      return new User(name, age);
    }

    private void SetName(string name)
    {
      Name = name;
    }

    private void SetAge(int age)
    {
      Age = age;
    }
  }
}
