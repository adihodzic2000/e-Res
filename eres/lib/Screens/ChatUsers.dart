import 'package:eres/Widgets/HeaderWidget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';

import '../Providers/baseProvider.dart';
import 'Chat.dart';

class ChatUsers extends StatefulWidget {
  static const String routeName = "/chatUsers";
  const ChatUsers({Key? key}) : super(key: key);

  @override
  State<ChatUsers> createState() => _ChatUsersState();
}

TextEditingController search = new TextEditingController();

class _ChatUsersState extends State<ChatUsers> {
  late BaseProvider _baseProvider;
  List<dynamic> users = [];
  String message = "Loading...";
  List<dynamic> unclickedMessages = [];
  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadUsers();
    super.initState();
  }

  Future getMessagesNumber() async {
    _baseProvider.setEndPoint("/api/Chat/get-unclicked-messages");
    var tmpData = await _baseProvider.get();
    setState(() {
      unclickedMessages = tmpData['data'];
    });
  }

  Future loadUsers() async {
    _baseProvider.setEndPoint("/api/Chat/get-my-users");
    final request = {"name": search.text};
    var tmpData = await _baseProvider.insert(request);
    setState(() {
      users = tmpData['data'];
      if (users.length == 0) {
        message = "Nemate aktivnih korisnika";
      }
    });
    await getMessagesNumber();
  }

  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
        Id: 0,
        child: Column(
          children: [
            Container(
              margin: EdgeInsets.all(10),
              child: Align(
                alignment: Alignment.centerLeft,
                child: Text(
                  "Ponuđeni korisnici",
                  style: TextStyle(color: Colors.white, fontSize: 15),
                ),
              ),
            ),
            Container(
              margin: EdgeInsets.fromLTRB(30, 20, 30, 10),
              child: TextField(
                  onChanged: (value) async {
                    await loadUsers();
                  },
                  style: TextStyle(color: Colors.white),
                  controller: search,
                  decoration: InputDecoration(
                    hintText: "Pretražite korisnika...",
                    hintStyle: TextStyle(color: Colors.white),
                    border: OutlineInputBorder(
                      borderSide: BorderSide(
                        color: Colors.white,
                      ),
                    ),
                    enabledBorder: OutlineInputBorder(
                      borderSide: BorderSide(
                        color: Colors.white,
                      ),
                    ),
                    focusedBorder: OutlineInputBorder(
                      borderSide: BorderSide(
                        color: Colors.white,
                      ),
                    ),
                  )),
            ),
            SingleChildScrollView(
              child: Column(
                children: buildUsers(),
              ),
            )
          ],
        ));
  }

  List<Widget> buildUsers() {
    if (users.length == 0) {
      return [
        Text(
          message,
          style: TextStyle(color: Colors.white),
        )
      ];
    }

    List<Widget> list = users
        .map((x) => Container(
              margin: EdgeInsets.fromLTRB(30, 5, 30, 5),
              height: 50,
              width: double.infinity,
              decoration: BoxDecoration(
                  color: Color.fromARGB(255, 14, 75, 108),
                  border: Border.all(width: 0),
                  borderRadius: BorderRadius.circular(10)),
              child: InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => Chat(
                        UserId: x['id'],
                      ),
                    ),
                  );
                },
                child: Row(children: [
                  SizedBox(
                    width: 10,
                  ),
                  ClipRRect(
                    borderRadius: BorderRadius.circular(100),
                    child: Image.network(
                      BaseProvider.GetUrl() + x['image']['path'],
                      width: 30,
                      height: 30,
                      fit: BoxFit.fill,
                    ),
                  ),
                  SizedBox(
                    width: 10,
                  ),
                  Text(
                    x['firstName'] +
                        " " +
                        x['lastName'] +
                        "" +
                        (x['company'].toString() == "null"
                            ? " "
                            : " - " + x['company']['title'].toString()) +
                        getNumberOfMessagesByUser(x['id']),
                    style: TextStyle(color: Colors.white),
                  )
                ]),
              ),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }

  String getNumberOfMessagesByUser(x) {
    int length = 0;
    for (var i = 0; i < unclickedMessages.length; i++) {
      if (unclickedMessages[i]['userFrom']['id'].toString() == x) {
        length++;
      }
    }
    if (length == 0) return '';

    return " (" + length.toString() + ")";
  }
}
