import 'dart:async';

import 'package:eres/Providers/baseProvider.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import 'MasterScreenWidget.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';

class HeaderWidget extends StatefulWidget {
  Widget? child;
  final Id;
  HeaderWidget({this.child, Key? key, this.Id}) : super(key: key);

  @override
  State<HeaderWidget> createState() => _HeaderWidgetState();
}

class _HeaderWidgetState extends State<HeaderWidget> {
  late FlutterLocalNotificationsPlugin flutterLocalNotificationsPlugin;
  Timer? timer;
  BaseProvider? _baseProvider;
  List<dynamic> _unseenMessages = [];
  var n = 0;
  @override
  void initState() {
    super.initState();
    _baseProvider = context.read<BaseProvider>();

    flutterLocalNotificationsPlugin = new FlutterLocalNotificationsPlugin();
    var android = new AndroidInitializationSettings('@mipmap/ic_launcher');
    var iOS = new IOSInitializationSettings();
    var initSetttings = new InitializationSettings(android: android, iOS: iOS);
    flutterLocalNotificationsPlugin.initialize(initSetttings,
        onSelectNotification: onSelectNotification);
    timer =
        Timer.periodic(Duration(seconds: 30), (Timer t) => showNotification());
  }

  void onSelectNotification(String? payload) {
    showDialog(
      context: context,
      builder: (_) => new AlertDialog(
        title: new Text('eRes'),
        content: new Text('$payload'),
      ),
    );
  }

  showNotification() async {
    if (BaseProvider.userData != null) {
      if (mounted) await loadMessages();
      var android = new AndroidNotificationDetails('channel id', 'channel NAME',
          priority: Priority.high, importance: Importance.max);
      var iOS = new IOSNotificationDetails();
      var platform = new NotificationDetails(android: android, iOS: iOS);
      for (var i = 0; i < _unseenMessages.length; i++) {
        await flutterLocalNotificationsPlugin.show(
            n++,
            "E-RES",
            _unseenMessages[i]['userFrom']['firstName'] +
                " " +
                _unseenMessages[i]['userFrom']['lastName'],
            platform,
            payload: _unseenMessages[i]['content']);
      }
    } else {}
  }

  @override
  void dispose() {
    timer?.cancel();
    super.dispose();
  }

  Future loadMessages() async {
    _baseProvider?.setEndPoint("/api/Chat/get-unseen-messages");
    var tmpData = await _baseProvider?.get();
    if (tmpData != null) {
      if (tmpData['status'].toString() == "401") {
        return;
      }
      if (mounted) {
        setState(() {
          try {
            _unseenMessages = tmpData['data'];
          } catch (error) {}
        });
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    return MasterScreenWidget(
      currentIndex: widget.Id,
      child: SingleChildScrollView(
          child: Column(
        children: [
          widget.child!,
        ],
      )),
    );
  }
}
