import 'package:eres/Models/Room.dart';
import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Screens/BillsDetails.dart';
import 'package:eres/Screens/Chat.dart';
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

class Bills extends StatefulWidget {
  static const String routeName = "/bills";
  const Bills({Key? key}) : super(key: key);

  @override
  State<Bills> createState() => _BillsState();
}

List<dynamic> bills = [];

class _BillsState extends State<Bills> {
  late BaseProvider _baseProvider;
  String message = "Loading...";
  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadData();
    super.initState();
  }

  Future loadData() async {
    _baseProvider.setEndPoint("/api/Bills/get-bills-by-logged-user");
    var tmpData = await _baseProvider.get();
    setState(() {
      bills = tmpData['data'];
      if (bills.length == 0) {
        message = "Nemate računa";
      }
    });
  }

  var formatter = NumberFormat("###.00", "en_US");
  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
        Id: 0,
        child: SingleChildScrollView(
          child: Column(children: [
            SizedBox(
              height: 10,
            ),
            Row(
              children: [
                SizedBox(
                  width: 10,
                ),
                Text("Plaćeno", style: TextStyle(color: Colors.white)),
                Container(
                  margin: EdgeInsets.all(5),
                  width: 10,
                  height: 10,
                  decoration: BoxDecoration(
                      color: Colors.green,
                      borderRadius: BorderRadius.all(Radius.circular(10))),
                ),
                SizedBox(
                  width: 5,
                ),
                Text(
                  "Nije plaćeno",
                  style: TextStyle(color: Colors.white),
                ),
                Container(
                  margin: EdgeInsets.all(5),
                  width: 10,
                  height: 10,
                  decoration: BoxDecoration(
                      color: Colors.orange,
                      borderRadius: BorderRadius.all(Radius.circular(10))),
                ),
              ],
            ),
            Column(children: buildBills())
          ]),
        ));
  }

  List<Widget> buildBills() {
    if (bills.length == 0) {
      return [
        Text(
          message,
          style: TextStyle(color: Colors.white),
        )
      ];
    }
    List<Widget> list = bills
        .map((e) => Container(
              margin: EdgeInsets.fromLTRB(10, 2, 10, 2),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(50),
                color: e['isPaid'].toString() == "false"
                    ? Colors.orange
                    : Colors.green,
              ),
              width: double.infinity,
              height: 50,
              child: TextButton(
                  onPressed: () {
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => BillsDetails(
                          Id: e['id'],
                          TotalAmount:
                              formatter.format(e['totalAmount']).toString(),
                          CompanyName: e['company']['title'],
                          Paid: e['isPaid'].toString(),
                          CompanyId: e['company']['id'],
                        ),
                      ),
                    );
                  },
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      Text(
                        e['company']['title'],
                        style: TextStyle(color: Colors.white, fontSize: 20),
                      ),
                      Text(
                        formatter.format(e['totalAmount']).toString(),
                        style: TextStyle(
                            color: e['isPaid'].toString() == "false"
                                ? Colors.red
                                : Colors.greenAccent,
                            fontSize: 24),
                      ),
                    ],
                  )),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }
}
