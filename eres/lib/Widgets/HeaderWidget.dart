import 'package:flutter/material.dart';

import 'MasterScreenWidget.dart';

class HeaderWidget extends StatefulWidget {
  Widget? child;
  final Id;
  HeaderWidget({this.child, Key? key, this.Id}) : super(key: key);

  @override
  State<HeaderWidget> createState() => _HeaderWidgetState();
}

class _HeaderWidgetState extends State<HeaderWidget> {
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
