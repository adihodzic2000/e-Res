import 'package:eres/Screens/Bills.dart';
import 'package:eres/Screens/ChatUsers.dart';
import 'package:eres/Screens/Companies.dart';
import 'package:eres/Screens/HomePage.dart';
import 'package:eres/Screens/MyPlaces.dart';
import 'package:eres/main.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import '../Providers/baseProvider.dart';

class eResDrawer extends StatefulWidget {
  const eResDrawer({Key? key}) : super(key: key);

  @override
  State<eResDrawer> createState() => _eResDrawerState();
}

class _eResDrawerState extends State<eResDrawer> {
  late BaseProvider baseProvider;
  late String MessagesNumber = '';

  @override
  void initState() {
    baseProvider = context.read<BaseProvider>();
    getMessagesNumber();
    super.initState();
  }

  Future getMessagesNumber() async {
    baseProvider.setEndPoint("/api/Chat/get-unclicked-messages");
    var tmpData = await baseProvider.get();
    if (tmpData != null) {
      if (mounted) {
        setState(() {
          if (tmpData['data'].length > 0)
            MessagesNumber = " (" + tmpData['data'].length.toString() + ")";
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Drawer(
      backgroundColor: Color.fromARGB(255, 14, 75, 108),
      child: ListView(
        padding: EdgeInsets.zero,
        children: [
          Container(
            margin: EdgeInsets.all(10),
            child: Row(
              children: [
                ClipRRect(
                  borderRadius: BorderRadius.circular(100),
                  child: Image.network(
                    BaseProvider.GetUrl() +
                        BaseProvider.userData['profileImagePath'],
                    width: 50,
                    height: 50,
                    fit: BoxFit.fill,
                  ),
                ),
                SizedBox(
                  width: 10,
                ),
                Text(
                  BaseProvider.userData['firstName'] +
                      " " +
                      BaseProvider.userData['lastName'],
                  style: TextStyle(color: Colors.white),
                ),
              ],
            ),
          ),
          Container(
            padding: EdgeInsets.all(10),
            child: Text(
              "MENU",
              style: TextStyle(color: Colors.grey, fontWeight: FontWeight.w600),
            ),
          ),
          ListTile(
            leading: Icon(
              Icons.filter_center_focus,
              color: Colors.white,
            ),
            title: Text(
              'Filtriraj podatke',
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              setState(() {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => HomePage(
                      IsVisibleFiltering: true,
                    ),
                  ),
                );
              });
            },
            selectedTileColor: Colors.green,
          ),
          // ListTile(
          //   leading: Icon(
          //     color: Colors.white,
          //     Icons.apartment,
          //   ),
          //   title: Text(
          //     'Apartmani',
          //     style: TextStyle(color: Colors.white),
          //   ),
          //   onTap: () {
          //     setState(() {
          //       // Navigator.popAndPushNamed(context, MyQRCode.routeName);
          //     });
          //   },
          // ),
          // ListTile(
          //   leading: Icon(
          //     color: Colors.white,
          //     Icons.hotel,
          //   ),
          //   title: Text(
          //     'Hoteli',
          //     style: TextStyle(color: Colors.white),
          //   ),
          //   onTap: () {
          //     // Navigator.popAndPushNamed(context, MyProgress.routeName);
          //   },
          // ),
          ListTile(
            leading: Icon(color: Colors.white, Icons.hotel),
            title: Text(
              'SVI OBJEKTI',
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.pushNamed(context, Companies.routeName);
            },
          ),
          ListTile(
            leading: Icon(color: Colors.white, Icons.payment),
            title: Text(
              'Računi',
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.pushNamed(context, Bills.routeName);
            },
          ),
          ListTile(
            leading: Icon(
              color: Colors.white,
              Icons.share_arrival_time,
            ),
            title: Text(
              'Moji obilasci',
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.popAndPushNamed(context, MyPlaces.routeName);
            },
          ),
          ListTile(
            leading: Icon(
              color: Colors.white,
              Icons.chat,
            ),
            title: Text(
              'CHAT' + MessagesNumber,
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              Navigator.popAndPushNamed(context, ChatUsers.routeName);
            },
          ),
          ListTile(
            tileColor: Colors.red,
            leading: Icon(
              color: Colors.white,
              Icons.logout,
            ),
            title: Text(
              'Odjavi se',
              style: TextStyle(color: Colors.white),
            ),
            onTap: () {
              BaseProvider.token = "";
              BaseProvider.userData = "";
              Navigator.popAndPushNamed(context, MyHomePage.routeName);
            },
          ),
        ],
      ),
    ));
  }
}
