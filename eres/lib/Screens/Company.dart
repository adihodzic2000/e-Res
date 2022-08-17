import 'package:eres/Models/Room.dart';
import 'package:eres/Providers/baseProvider.dart';
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

class Company extends StatefulWidget {
  static const String routeName = "/company";
  final String Id;
  Company({Key? key, required this.Id}) : super(key: key);

  @override
  State<Company> createState() => _CompanyState();
}

class _CompanyState extends State<Company> {
  List<dynamic> images = [];
  List<Room> rooms = [];

  Room? selectedRoom;
  dynamic roomDetails;
  String roomPrice = "0";
  int days = 0;
  bool IsVisibleTotalAmount = false;
  late BaseProvider _baseProvider;
  dynamic _companyData;
  String message = "";
  String? guestId;

  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadImages();
    getCompanyData();
    loadRooms();
    super.initState();
  }

  Future loadImages() async {
    _baseProvider.setEndPoint("/api/File/get-images/${widget.Id}");
    var tmpData = await _baseProvider.get();
    setState(() {
      images = tmpData['data'];
    });
  }

  Future getCompanyData() async {
    _baseProvider.setEndPoint("/api/Company/get-company/${widget.Id}");
    var tmpData = await _baseProvider.get();
    setState(() {
      _companyData = tmpData['data'];
    });
  }

  Future loadRooms() async {
    _baseProvider.setEndPoint("/api/Room/get-rooms-by-company-id");
    final request = {'id': widget.Id, 'byName': ''};
    var tmpData = await _baseProvider.insert(request);
    setState(() {
      rooms.clear();

      rooms.addAll(
          tmpData['data'].map((x) => Room.fromJson(x)).cast<Room>().toList());
      selectedRoom = rooms[0];
    });
    loadPrice();
  }

  Future loadPrice() async {
    _baseProvider.setEndPoint("/api/Room/get-room/${selectedRoom?.id}");

    var tmpData = await _baseProvider.get();
    setState(() {
      roomDetails = tmpData['data'];
      var n = NumberFormat("###.00", "en_US");

      roomPrice = n.format(roomDetails['price']).toString();
    });
  }

  Future callTotalAmount() async {
    if (dateCtl.text.isNotEmpty && dateCtl2.text.isNotEmpty) {
      setState(() {
        IsVisibleTotalAmount = true;
        days = -DateTime(
                int.parse(dateCtl.text.substring(6, 10)),
                int.parse(dateCtl.text.substring(3, 5)),
                int.parse(dateCtl.text.substring(0, 2)))
            .difference(DateTime(
                int.parse(dateCtl2.text.substring(6, 10)),
                int.parse(dateCtl2.text.substring(3, 5)),
                int.parse(dateCtl2.text.substring(0, 2))))
            .inDays;
      });
    }
  }

  Future deleteGuestForCompany() async {
    _baseProvider.setEndPoint("/api/Guest/delete-guest");
    await _baseProvider.removeById(guestId!);
  }

  Future createGuestForCompany() async {
    _baseProvider.setEndPoint("/api/Guest/add-guest");
    final request = {
      "firstName": BaseProvider.userData['firstName'],
      "lastName": BaseProvider.userData['lastName'],
      "phoneNumber": BaseProvider.userData['phoneNumber'],
      "companyId": widget.Id
    };
    var tmpData = await _baseProvider.insert(request);
    setState(() {
      guestId = tmpData['data'];
    });
    createReservation();
  }

  Future createReservation() async {
    try {
      _baseProvider.setEndPoint("/api/Reservation/create-reservation");
      final request = {
        "dateFrom": DateTime(
                int.parse(dateCtl.text.substring(6, 10)),
                int.parse(dateCtl.text.substring(3, 5)),
                int.parse(dateCtl.text.substring(0, 2)))
            .toString(),
        "dateTo": DateTime(
                int.parse(dateCtl2.text.substring(6, 10)),
                int.parse(dateCtl2.text.substring(3, 5)),
                int.parse(dateCtl2.text.substring(0, 2)))
            .toString(),
        "roomId": selectedRoom?.id,
        "guestId": guestId
      };
      var tmpData = await _baseProvider.insert(request);
      if (tmpData['status'].toString() != "200") {
        deleteGuestForCompany();
      }
      setState(() {
        message = tmpData['info'];
      });
    } on Exception catch (_) {
      print("Niste izabrali datume");
    } catch (error) {
      print("Niste izabrali datume");
    }
  }

  TextEditingController price = new TextEditingController();
  TextEditingController dateCtl = TextEditingController();
  TextEditingController dateCtl2 = TextEditingController();
  @override
  Widget build(BuildContext context) {
    var a = NumberFormat("###.00", "en_US");

    DateFormat formatter = DateFormat('dd.MM.yyyy');
    return HeaderWidget(
      Id: 0,
      child: SingleChildScrollView(
          child: Column(children: [
        Container(
            height: 200,
            child: GridView(
              gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                  crossAxisCount: 1,
                  childAspectRatio: 1,
                  crossAxisSpacing: 10,
                  mainAxisSpacing: 10),
              scrollDirection: Axis.horizontal,
              children: buildImages(),
            )),
        Center(
          child: Column(children: [
            Text(
              _companyData == null ? "" : _companyData['title'],
              style: TextStyle(color: Colors.green, fontSize: 15),
            ),
            InkWell(
              onTap: () {
                Navigator.pushNamed(context, Maps.routeName);
              },
              child: Text(
                _companyData == null
                    ? ""
                    : _companyData['location']['city']['title'] +
                        ", " +
                        _companyData['location']['city']['country']['title'] +
                        "(Vidi mapu)",
                style: TextStyle(color: Colors.white, fontSize: 15),
              ),
            ),
          ]),
        ),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceAround,
          children: [
            ElevatedButton(
              style: ButtonStyle(
                backgroundColor: MaterialStateProperty.all(Colors.teal),
                shape: MaterialStateProperty.all<RoundedRectangleBorder>(
                  RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(18.0),
                    side: BorderSide(
                      color: Colors.teal,
                      width: 0.5,
                    ),
                  ),
                ),
              ),
              child: Text('Pošalji upit'),
              onPressed: () async {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => Chat(
                      UserId: widget.Id,
                    ),
                  ),
                );
              },
            ),
            DropdownButton<Room>(
              value: selectedRoom,
              dropdownColor: Colors.black,
              onChanged: (Room? newValue) {
                setState(() {
                  selectedRoom = newValue!;
                  loadPrice();
                });
              },
              items: rooms.map((Room country) {
                return new DropdownMenuItem<Room>(
                  value: country,
                  child: new Text(
                    country.title,
                    style: new TextStyle(color: Colors.white),
                  ),
                );
              }).toList(),
            ),
            Text(
              roomPrice + "KM",
              style: TextStyle(color: Colors.white),
            )
          ],
        ),
        Container(
            margin: EdgeInsets.fromLTRB(10, 10, 10, 0),
            child: Row(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              children: [
                Expanded(
                    child: TextFormField(
                  controller: dateCtl,
                  style: TextStyle(color: Colors.white),
                  decoration: InputDecoration(
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
                    labelText: "Datum od",
                    labelStyle: TextStyle(color: Colors.white),
                    hintText: "Izaberite datum",
                  ),
                  onTap: () async {
                    DateTime date = DateTime(1900);
                    FocusScope.of(context).requestFocus(new FocusNode());

                    date = await showDatePicker(
                            context: context,
                            initialDate: DateTime.now(),
                            firstDate: DateTime(1900),
                            lastDate: DateTime(2100)) ??
                        DateTime.now();

                    dateCtl.text = formatter.format(date);
                    callTotalAmount();
                  },
                )),
                SizedBox(
                  width: 5,
                ),
                Expanded(
                    child: TextFormField(
                  controller: dateCtl2,
                  style: TextStyle(color: Colors.white),
                  decoration: InputDecoration(
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
                    labelText: "Datum do",
                    labelStyle: TextStyle(color: Colors.white),
                    hintText: "Izaberite datum",
                  ),
                  onTap: () async {
                    DateTime date = DateTime(1900);
                    FocusScope.of(context).requestFocus(new FocusNode());

                    date = await showDatePicker(
                            context: context,
                            initialDate: DateTime.now(),
                            firstDate: DateTime(1900),
                            lastDate: DateTime(2100)) ??
                        DateTime.now();

                    dateCtl2.text = formatter.format(date);
                    callTotalAmount();
                  },
                ))
              ],
            )),
        SizedBox(
          height: 5,
        ),
        Visibility(
            visible: IsVisibleTotalAmount,
            child: Center(
                child: Text(
              "UKUPNO: ${a.format(double.parse(roomPrice) * days)}",
              style: TextStyle(color: Colors.white),
            ))),
        ElevatedButton(
          style: ButtonStyle(
            backgroundColor: MaterialStateProperty.all(Colors.green),
            shape: MaterialStateProperty.all<RoundedRectangleBorder>(
              RoundedRectangleBorder(
                borderRadius: BorderRadius.circular(18.0),
                side: BorderSide(
                  color: Colors.green,
                  width: 0.5,
                ),
              ),
            ),
          ),
          child: Text('REZERVIŠI'),
          onPressed: () {
            createGuestForCompany();
          },
        ),
        Center(
          child: Text(
            message,
            style: TextStyle(color: Colors.white),
          ),
        )
      ])),
    );
  }

  List<Widget> buildImages() {
    if (images.length == 0) {
      return [
        Text(
          "Loading...",
          style: TextStyle(color: Colors.white),
        )
      ];
    }

    List<Widget> list = images
        .map((x) => Container(
              margin: EdgeInsets.fromLTRB(10, 10, 0, 10),
              color: Colors.white,
              height: 100,
              width: 100,
              child: Image.network(
                  width: 100, height: 100, BaseProvider.GetUrl() + x['path']),
            ))
        .cast<Widget>()
        .toList();

    return list;
  }
}
