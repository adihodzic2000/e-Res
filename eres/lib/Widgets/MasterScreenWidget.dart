import 'package:eres/Screens/HomePage.dart';
import 'package:eres/Widgets/eResDrawer.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';

class MasterScreenWidget extends StatefulWidget {
  Widget? child;
  final currentIndex;
  MasterScreenWidget({this.child, Key? key, this.currentIndex})
      : super(key: key);

  @override
  State<MasterScreenWidget> createState() => _MasterScreenWidgetState();
}

class _MasterScreenWidgetState extends State<MasterScreenWidget> {
  final scaffoldKey = GlobalKey<ScaffoldState>();
  @override
  void initState() {
    super.initState();
  }

  void _onItemTapped(int index) {
    setState(() {
      if (index == 0) {
        Navigator.popAndPushNamed(context, HomePage.routeName);
      } else {
        // Notifications();
        // Navigator.popAndPushNamed(context, Notifications.routeName);
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      key: scaffoldKey,
      appBar: AppBar(
        automaticallyImplyLeading: false,
        title: Row(
          mainAxisAlignment: MainAxisAlignment.start,
          children: [
            Align(
              child: Text("ERES"),
            )
          ],
        ),
        actions: [
          IconButton(
            onPressed: () {
              scaffoldKey.currentState?.openDrawer();
            },
            icon: Icon(Icons.menu),
          ),
        ],
      ),
      drawer: eResDrawer(),
      body: SafeArea(
        child: widget.child!,
      ),
      bottomNavigationBar: BottomNavigationBar(
        backgroundColor: Color.fromARGB(255, 0, 38, 54),
        unselectedItemColor: Colors.white,
        items: const <BottomNavigationBarItem>[
          BottomNavigationBarItem(
            icon: Icon(Icons.home),
            label: 'Početna',
          ),
          BottomNavigationBarItem(
            icon: Icon(Icons.account_circle),
            label: 'Profil',
          ),
        ],
        currentIndex: widget.currentIndex,
        onTap: _onItemTapped,
        selectedItemColor: Color.fromARGB(255, 0, 137, 192),
      ),
    );
  }
}
