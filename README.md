# SimpleMessageBus
A simple Message Bus implementation for use with .Net 3.5 (Unity Game Engine)

##Latest Release
The latest compiled release can be found [here](https://drive.google.com/file/d/0B7q5jPppblzUb2F6amc4bGFMMlE/view?usp=sharing)

##About
SimpleMessageBus can be used with any C# .Net3.5+ project, but it was specifically created for use with Unity projects.  There are 3 parts to the system: the MessageBus, the IMessage implementing message classes, and the message recipient classes.
* The MessageBus keeps a record of all classes that are registered to receive a certain message class type.  When a message is sent, each registered recipient is sent a copy of that message to act upon.  
* Classes must register themselves with the MessageBus to receive a certain message class.
* The message classes implement the IMessage interface, and allow for transfering data between the sending class and the receiving class.

##How-To
Using SimpleMessageBus with a Unity project:
* Import SimpleMessageBus as an Asset into the Unity project (within Unity)
* Create a message class
** The message class must inherit from IMessage
** The message class can take parameters (messageText below) that are passed on to the recipients of the message
```c#
using SimpleMessageBus

namespace Messages {
  public class SimpleMessageClass : IMessage {
    public SimpleMessageClass(string messageText) {
      this.Text = messageText;
    }
    
    public string Text { get; private set; }
  }
}
```
* Register classes to receive your new message class
```c#
MessageBus.Instance.Register<SimpleMessageClass>(this, msg => {
  if(string.IsNullOrEmpty(msg.Text)) return;
  Debug.Log(msg.Text);
});
```
* From another class, send your new message
```c#
MessageBus.Instance.Send(new SimpleMessageClass("Hello Unity"));
```

##Credits/Acknowledgements
* MessageBus use with Unity idea from https://github.com/franciscotufro/message-bus-pattern
* MessageBus implementation similar to that found in [MVVM Light Framework Messenger class](http://mvvmlight.codeplex.com/SourceControl/latest#GalaSoft.MvvmLight/GalaSoft.MvvmLight%20%28PCL%29/Messaging/Messenger.cs) (Not nearly as sophisticated though)
* WeakAction class similar to that found in [MVVM Light Framework](http://mvvmlight.codeplex.com/SourceControl/latest#GalaSoft.MvvmLight/GalaSoft.MvvmLight%20%28PCL%29/Helpers/WeakAction.cs)
* Extension method to check if class implements interface from https://bradhe.wordpress.com/2010/07/27/how-to-tell-if-a-type-implements-an-interface-in-net/
and also from http://stackoverflow.com/a/5976618


