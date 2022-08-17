import 'dart:async';

import 'package:eres/Models/Room.dart';
import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Screens/Maps.dart';
import 'package:eres/Widgets/HeaderWidget.dart';
import 'package:eres/Widgets/MyCustomForm.dart';
import 'package:eres/Widgets/MyCustomFormText.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class Chat extends StatefulWidget {
  static const String routeName = "/chat";
  final String UserId;

  const Chat({Key? key, required this.UserId}) : super(key: key);

  @override
  State<Chat> createState() => _ChatState();
}

List<dynamic> messages = [];

class _ChatState extends State<Chat> {
  late BaseProvider _baseProvider;
  Timer? timer;
  String Id = "";
  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadUser();
    timer = Timer.periodic(Duration(seconds: 15), (Timer t) => loadMessages());
    super.initState();
  }

  Future loadUser() async {
    _baseProvider.setEndPoint("/api/User/get-user/${widget.UserId}");
    var tmpData = await _baseProvider.get();
    setState(() {
      Id = tmpData['data'].toString();
    });
    loadMessages();
  }

  @override
  void dispose() {
    timer?.cancel();
    super.dispose();
  }

  Future loadMessages() async {
    try {
      _baseProvider.setEndPoint(
          "/api/Chat/get-messages/${BaseProvider.userData['userId']}/${Id}");
      var tmpData = await _baseProvider.get();
      setState(() {
        messages = tmpData['data'];
      });
    } catch (error) {
      //socket exception
    }
    try {
      _baseProvider.setEndPoint("/api/Chat/see-unclicked-messages");
      await _baseProvider.update(widget.UserId);
    } catch (error) {}
  }

  TextEditingController sendMessage = new TextEditingController();
  Future createMessage() async {
    _baseProvider.setEndPoint("/api/Chat/create-message");
    final request = {
      "userFromId": BaseProvider.userData['userId'],
      "userToId": Id,
      "content": sendMessage.text
    };
    var tmpData = await _baseProvider.insert(request);
    setState(() {
      sendMessage.text = "";
    });
    await loadMessages();
  }

  @override
  Widget build(BuildContext context) {
    DateFormat formatter = DateFormat('dd.MM.yyyy HH:mm');

    return HeaderWidget(
        Id: 0,
        child: SingleChildScrollView(
          child: SizedBox(
            height: 550,
            child: Column(children: [
              // Text(
              //   "Adi Hodzic",
              //   style: TextStyle(color: Colors.white),
              // ),
              Expanded(
                  child: Container(
                padding: EdgeInsets.symmetric(horizontal: 20),
                decoration: BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.only(
                        topLeft: Radius.circular(30),
                        topRight: Radius.circular(30))),
                child: ClipRRect(
                  borderRadius: BorderRadius.only(
                      topLeft: Radius.circular(30),
                      topRight: Radius.circular(30)),
                  child: ListView.builder(
                      reverse: true,
                      itemCount: messages.length,
                      itemBuilder: (context, int index) {
                        final message = messages[index];
                        bool isMe = message['userFromId'].toString() ==
                            BaseProvider.userData['userId'];
                        return Container(
                            margin: EdgeInsets.only(top: 10),
                            child: Column(
                              children: [
                                Row(
                                    mainAxisAlignment: isMe
                                        ? MainAxisAlignment.end
                                        : MainAxisAlignment.start,
                                    crossAxisAlignment: isMe
                                        ? CrossAxisAlignment.end
                                        : CrossAxisAlignment.start,
                                    children: [
                                      if (!isMe)
                                        Column(
                                          children: [
                                            Text(message['userFrom']
                                                ['firstName']),
                                            CircleAvatar(
                                              radius: 15,
                                              backgroundImage: NetworkImage(
                                                  BaseProvider.GetUrl() +
                                                      message['userFrom']
                                                          ['image']['path']),
                                            ),
                                          ],
                                        ),
                                      SizedBox(
                                        width: 20,
                                      ),
                                      Container(
                                          padding: EdgeInsets.all(10),
                                          constraints: BoxConstraints(
                                              maxWidth: MediaQuery.of(context)
                                                      .size
                                                      .width *
                                                  0.6),
                                          decoration: BoxDecoration(
                                              color: isMe
                                                  ? Colors.green
                                                  : Colors.grey[200],
                                              borderRadius: BorderRadius.only(
                                                topLeft: Radius.circular(16),
                                                topRight: Radius.circular(16),
                                                bottomLeft: Radius.circular(
                                                    isMe ? 12 : 0),
                                                bottomRight: Radius.circular(
                                                    isMe ? 0 : 12),
                                              )),
                                          child: Text(
                                            messages[index]['content'],
                                            style: TextStyle(
                                                color: isMe
                                                    ? Colors.white
                                                    : Colors.grey[800]),
                                          ))
                                    ]),
                                Padding(
                                  padding: const EdgeInsets.only(top: 5),
                                  child: Row(
                                    mainAxisAlignment: isMe
                                        ? MainAxisAlignment.end
                                        : MainAxisAlignment.start,
                                    children: [
                                      if (!isMe)
                                        SizedBox(
                                          width: 40,
                                        ),
                                      Icon(
                                        Icons.done_all,
                                        size: 20,
                                        color: Colors.grey,
                                      ),
                                      SizedBox(
                                        width: 8,
                                      ),
                                      Text(formatter.format(DateTime.parse(
                                          message['createdDate'])))
                                    ],
                                  ),
                                )
                              ],
                            ));
                      }),
                ),
              )),
              Container(
                color: Colors.white,
                height: 100,
                child: Row(children: [
                  Expanded(
                      child: Container(
                    padding: EdgeInsets.symmetric(horizontal: 20),
                    height: 60,
                    margin: EdgeInsets.all(20),
                    decoration: BoxDecoration(
                      borderRadius: BorderRadius.circular(30),
                      color: Colors.grey[200],
                    ),
                    child: Row(
                      children: [
                        SizedBox(
                          width: 10,
                        ),
                        Expanded(
                            child: TextField(
                          controller: sendMessage,
                          decoration: InputDecoration(
                              border: InputBorder.none,
                              hintText: "Ukucajte poruku...",
                              hintStyle: TextStyle(color: Colors.grey[500])),
                        )),
                        IconButton(
                            onPressed: () async {
                              await loadMessages();
                              await createMessage();
                            },
                            icon: Icon(Icons.send))
                      ],
                    ),
                  ))
                ]),
              )
            ]),
          ),
        ));
  }
}
