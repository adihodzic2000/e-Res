import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Widgets/HeaderWidget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import '../Widgets/MyCustomFormText.dart';
import 'Registration.dart';

class MyProfile extends StatefulWidget {
  static const routeName = "myProfile";
  const MyProfile({Key? key}) : super(key: key);

  @override
  State<MyProfile> createState() => _MyProfileState();
}

class _MyProfileState extends State<MyProfile> {
  TextEditingController _firstName = new TextEditingController();
  TextEditingController _lastName = new TextEditingController();
  TextEditingController _phoneNumber = new TextEditingController();
  TextEditingController _email = new TextEditingController();
  Gender selected = Gender.Male;
  late BaseProvider baseProvider;
  bool isVisibleMessage = false;
  String message = "";
  @override
  void initState() {
    super.initState();
    _firstName.text = BaseProvider.userData['firstName'];
    _lastName.text = BaseProvider.userData['lastName'];
    _phoneNumber.text = BaseProvider.userData['phoneNumber'];
    _email.text = BaseProvider.userData['email'];

    selected = BaseProvider.userData['gender'].toString() == "1"
        ? Gender.Male
        : Gender.Female;

    baseProvider = context.read<BaseProvider>();
  }

  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
      Id: 1,
      child: Column(children: [
        SizedBox(
          height: 10,
        ),
        Text(
          "Moj profil",
          style: TextStyle(color: Colors.white, fontSize: 20),
        ),
        SizedBox(
          height: 10,
        ),
        Row(children: [
          Expanded(
            child: Container(
              child: MyCustomFormText(
                "Ime",
                _firstName,
                true,
                IsObsecured: false,
              ),
            ),
          ),
          Expanded(
            child: Container(
              child: MyCustomFormText(
                "Prezime",
                _lastName,
                true,
                IsObsecured: false,
              ),
            ),
          ),
        ]),
        Row(children: [
          Expanded(
            child: Container(
              child: MyCustomFormText(
                "Broj telefona",
                _phoneNumber,
                true,
                IsObsecured: false,
              ),
            ),
          ),
          Expanded(
              child: Theme(
            data: ThemeData(
              unselectedWidgetColor: Colors.green,
            ),
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                SizedBox(
                  height: 20,
                ),
                RadioListTile<Gender>(
                  title: const Text(
                    'Muško',
                    style: TextStyle(color: Colors.white),
                  ),
                  value: Gender.Male,
                  visualDensity: const VisualDensity(
                    horizontal: VisualDensity.minimumDensity,
                    vertical: VisualDensity.minimumDensity,
                  ),
                  groupValue: selected,
                  dense: true,
                  onChanged: (Gender? value) {
                    setState(() {
                      selected = value!;
                    });
                  },
                ),
                RadioListTile<Gender>(
                  title: const Text('Žensko',
                      style: TextStyle(color: Colors.white)),
                  value: Gender.Female,
                  groupValue: selected,
                  controlAffinity: ListTileControlAffinity.leading,
                  visualDensity: const VisualDensity(
                    horizontal: VisualDensity.minimumDensity,
                    vertical: VisualDensity.minimumDensity,
                  ),
                  dense: true,
                  onChanged: (Gender? value) {
                    setState(() {
                      selected = value!;
                    });
                  },
                ),
              ],
            ),
          )),
        ]),
        Container(
          child: MyCustomFormText(
            "E-mail:",
            _email,
            true,
            IsObsecured: false,
          ),
        ),
        Container(
          margin: EdgeInsets.all(10),
          width: double.infinity,
          decoration: BoxDecoration(
            borderRadius: BorderRadius.circular(20),
            color: Colors.green,
          ),
          child: TextButton(
              onPressed: () async {
                if (_firstName.text.trim().isNotEmpty ||
                    _lastName.text.trim().isNotEmpty ||
                    _phoneNumber.text.trim().isNotEmpty ||
                    _email.text.trim().isNotEmpty) {
                  final request = {
                    "firstName": _firstName.text,
                    "lastName": _lastName.text,
                    "phoneNumber": _phoneNumber.text,
                    "email": _email.text,
                    "gender": selected.index + 1
                  };
                  baseProvider.setEndPoint("/api/User/update-user");
                  var tmpData = await baseProvider.update(
                      BaseProvider.userData['userId'], request);
                  print(tmpData);
                  if (tmpData['status'].toString() == "200") {
                    setState(() {
                      BaseProvider.userData['firstName'] = _firstName.text;
                      BaseProvider.userData['lastName'] = _lastName.text;
                      BaseProvider.userData['phoneNumber'] = _phoneNumber.text;
                      BaseProvider.userData['email'] = _email.text;
                      BaseProvider.userData['gender'] =
                          (selected.index + 1).toString();

                      isVisibleMessage = true;
                      message = "Uspješno ste spasili svoje postavke";
                    });
                  }
                } else {
                  setState(() {
                    isVisibleMessage = true;
                    message = "Niste ukucali sva polja";
                  });
                }
              },
              child: Text(
                "Izmjeni podatke",
                style: TextStyle(color: Colors.white),
              )),
        ),
        Visibility(
            visible: isVisibleMessage,
            child: Text(
              message,
              style: TextStyle(color: Colors.white),
            ))
      ]),
    );
  }
}
