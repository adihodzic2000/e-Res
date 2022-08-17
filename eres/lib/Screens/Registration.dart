import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Widgets/MyCustomForm.dart';
import 'package:eres/Widgets/MyCustomFormText.dart';
import 'package:eres/main.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';

class Registration extends StatefulWidget {
  static const String routeName = "/registration";
  const Registration({Key? key}) : super(key: key);

  @override
  State<Registration> createState() => _RegistrationState();
}

TextEditingController _firstName = new TextEditingController();
TextEditingController _lastName = new TextEditingController();
TextEditingController _phoneNumber = new TextEditingController();
TextEditingController _userName = new TextEditingController();
TextEditingController _email = new TextEditingController();
TextEditingController _password = new TextEditingController();
TextEditingController _repeatPassword = new TextEditingController();

enum Gender { Male, Female }

class _RegistrationState extends State<Registration> {
  Gender selected = Gender.Male;
  String message = "";
  bool IsMessageVisible = false;
  late BaseProvider baseProvider;

  @override
  void initState() {
    super.initState();
    baseProvider = context.read<BaseProvider>();
  }

  void clearForm() {
    setState(() {
      _firstName.text = "";
      _lastName.text = "";
      _email.text = "";
      _password.text = "";
      _repeatPassword.text = "";
      _userName.text = "";
      _phoneNumber.text = "";
    });
  }

  @override
  Widget build(BuildContext context) {
    return SafeArea(
        child: Scaffold(
      body: SingleChildScrollView(
        child: Column(
          children: [
            SizedBox(
              height: 10,
            ),
            Text(
              "Registracija",
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
                "Korisničko ime:",
                _userName,
                true,
                IsObsecured: false,
              ),
            ),
            Container(
              child: MyCustomFormText(
                "E-mail:",
                _email,
                true,
                IsObsecured: false,
              ),
            ),
            Row(children: [
              Expanded(
                child: Container(
                  child: MyCustomFormText(
                    "Šifra",
                    _password,
                    true,
                    IsObsecured: true,
                  ),
                ),
              ),
              Expanded(
                child: Container(
                  child: MyCustomFormText(
                    "Ponovite šifru",
                    _repeatPassword,
                    true,
                    IsObsecured: true,
                  ),
                ),
              ),
            ]),
            Container(
              margin: EdgeInsets.all(10),
              width: double.infinity,
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(20),
                color: Colors.green,
              ),
              child: TextButton(
                  onPressed: () async {
                    if (_firstName.text.trim().isEmpty ||
                        _lastName.text.trim().isEmpty ||
                        _phoneNumber.text.trim().isEmpty ||
                        _email.text.trim().isEmpty ||
                        _userName.text.trim().isEmpty ||
                        _password.text.trim().isEmpty ||
                        _repeatPassword.text.trim().isEmpty) {
                      setState(() {
                        IsMessageVisible = true;
                        message = "Niste ukucali sva polja!";
                      });
                    } else if (_password.text.length < 6 ||
                        _repeatPassword.text.length < 6) {
                      setState(() {
                        IsMessageVisible = true;
                        message = "Ukucajte 6 karaktera!";
                      });
                    } else if (_password.text != _repeatPassword.text) {
                      setState(() {
                        IsMessageVisible = true;
                        message = "Šifre se ne poklapaju!";
                      });
                    } else {
                      setState(() {
                        IsMessageVisible = false;
                        message = "";
                      });
                      try {
                        baseProvider.setEndPoint("/api/User");
                        final request = {
                          "firstName": _firstName.text,
                          "lastName": _lastName.text,
                          "phoneNumber": _phoneNumber.text,
                          "userName": _userName.text,
                          "email": _email.text,
                          "password": _password.text,
                          "gender": selected.index + 1,
                          "userRoles": ["CA39052B-C121-4C58-189D-08DA6E6E173C"]
                        };
                        var tmpData =
                            await baseProvider.insertWithoutToken(request);
                        print(tmpData);
                        if (tmpData['status'].toString() == "200") {
                          Navigator.pushNamed(context, MyHomePage.routeName);
                        }
                        clearForm();
                      } catch (error) {
                        IsMessageVisible = true;
                        message = error.toString();
                      }
                    }
                  },
                  child: Text(
                    "Registruj se",
                    style: TextStyle(color: Colors.white),
                  )),
            ),
            Visibility(
              visible: IsMessageVisible,
              child: Container(
                margin: EdgeInsets.all(10),
                child: Text(
                  message,
                  style: TextStyle(color: Colors.red),
                ),
              ),
            )
          ],
        ),
      ),
    ));
  }
}
