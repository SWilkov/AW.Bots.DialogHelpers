using System;
using System.Collections.Generic;
using System.Text;

namespace AW.Bots.DialogHelpers.Models
{
  public class DialogBotMessages
  {
    public string Name { get; set; }
    public ICollection<Message> Messages { get; set; }
  }
}
