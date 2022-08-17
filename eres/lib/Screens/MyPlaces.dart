import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';

import '../Providers/baseProvider.dart';
import '../Widgets/HeaderWidget.dart';
import 'Company.dart';

class MyPlaces extends StatefulWidget {
  static const String routeName = "/myplaces";
  const MyPlaces({Key? key}) : super(key: key);

  @override
  State<MyPlaces> createState() => _MyPlacesState();
}

class _MyPlacesState extends State<MyPlaces> {
  List<dynamic> companies = [];
  late BaseProvider _baseProvider;
  String message = "Loading...";
  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadData();
    super.initState();
  }

  Future loadData() async {
    _baseProvider.setEndPoint("/api/User/get-my-places");
    var tmpData = await _baseProvider.get();
    setState(() {
      companies = tmpData['data'];
      if (companies.length == 0) {
        message = "Nemate aktivnih korisnika";
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
        Id: 0,
        child: SingleChildScrollView(
          child: Container(
            color: Color.fromARGB(255, 0, 58, 82),
            width: double.infinity,
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                SizedBox(
                  height: 10,
                ),
                Column(
                  children: buildListOfCompanies(),
                )
              ],
            ),
          ),
        ));
  }

  List<Widget> buildListOfCompanies() {
    return companies
        .map(
          (e) => Container(
              margin: EdgeInsets.fromLTRB(0, 10, 0, 10),
              child: InkWell(
                onTap: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => Company(
                        Id: e['id'].toString(),
                      ),
                    ),
                  );
                },
                child: Column(
                  children: [
                    Container(
                      child: Image.network(
                        width: 380,
                        height: 150,
                        BaseProvider.GetUrl() + e['logo']['path'],
                        fit: BoxFit.fill,
                      ),
                    ),
                    Text(
                      e['title'],
                      style: TextStyle(fontSize: 20, color: Colors.white),
                    )
                  ],
                ),
              )),
        )
        .toList();
  }
}
