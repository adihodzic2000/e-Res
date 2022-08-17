import 'package:eres/main.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import '../Providers/baseProvider.dart';
import '../Widgets/MyCustomFormText.dart';

class ForgotPassword extends StatefulWidget {
  static const String routeName = "/forgotPassword";
  const ForgotPassword({Key? key}) : super(key: key);

  @override
  State<ForgotPassword> createState() => _ForgotPasswordState();
}

TextEditingController _email = new TextEditingController();
TextEditingController _code = new TextEditingController();
TextEditingController _password = new TextEditingController();
TextEditingController _repeatPassword = new TextEditingController();

bool IsEmailVisible = true;
bool IsCodeVisible = false;
bool IsPasswordVisible = false;
bool IsMessageVisible = false;

String message = "";
String ButtonMessage = "Pošalji email";

String userId = "";

class _ForgotPasswordState extends State<ForgotPassword> {
  late BaseProvider baseProvider;
  @override
  void initState() {
    super.initState();
    baseProvider = context.read<BaseProvider>();
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
            Container(
              margin: EdgeInsets.all(10),
              child: Text(
                "Za izmjenu lozinke molimo popunite sljedeća polja",
                style: TextStyle(color: Colors.white, fontSize: 20),
              ),
            ),
            SizedBox(
              height: 10,
            ),
            Visibility(
              visible: IsEmailVisible,
              child: Container(
                child: MyCustomFormText(
                  "E-mail:",
                  _email,
                  true,
                  IsObsecured: false,
                ),
              ),
            ),
            Visibility(
              visible: IsCodeVisible,
              child: Container(
                child: MyCustomFormText(
                  "Kod:",
                  _code,
                  true,
                  IsObsecured: false,
                ),
              ),
            ),
            Visibility(
              visible: IsPasswordVisible,
              child: Container(
                child: MyCustomFormText(
                  "Šifra",
                  _password,
                  true,
                  IsObsecured: true,
                ),
              ),
            ),
            Visibility(
              visible: IsPasswordVisible,
              child: Container(
                child: MyCustomFormText(
                  "Ponovite šifru",
                  _repeatPassword,
                  true,
                  IsObsecured: true,
                ),
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
                    if (IsEmailVisible) {
                      baseProvider.setEndPoint("/api/User/forgot-password");
                      final request = {"email": _email.text};
                      var tmpData =
                          await baseProvider.insertWithoutToken(request);
                      setState(() {
                        if (tmpData['status'].toString() == "200") {
                          IsEmailVisible = false;
                          IsCodeVisible = true;
                          IsPasswordVisible = false;
                          IsMessageVisible = false;

                          ButtonMessage = "Upišite kod";
                        } else {
                          IsMessageVisible = true;
                          message = "Pogrešan email";
                        }
                      });
                    } else if (IsCodeVisible) {
                      baseProvider.setEndPoint("/api/User/check-code");
                      final request = {
                        "email": _email.text,
                        "code": _code.text
                      };
                      var tmpData =
                          await baseProvider.insertWithoutToken(request);
                      setState(() {
                        if (tmpData['status'].toString() == "200") {
                          IsEmailVisible = false;
                          IsCodeVisible = false;
                          IsPasswordVisible = true;
                          IsMessageVisible = false;

                          userId = tmpData['data'].toString();
                          ButtonMessage = "Izmjeni šifru";
                        } else {
                          IsMessageVisible = true;
                          message = "Pogrešan kod";
                        }
                      });
                    } else if (IsPasswordVisible) {
                      if (_password.text == _repeatPassword.text &&
                          _password.text.length >= 6) {
                        baseProvider.setEndPoint("/api/User/change-password");
                        final request = {
                          "id": userId,
                          "password": _password.text
                        };
                        var tmpData =
                            await baseProvider.insertWithoutToken(request);
                        setState(() {
                          if (tmpData['status'].toString() == "200") {
                            IsEmailVisible = false;
                            IsCodeVisible = false;
                            IsPasswordVisible = false;
                            IsMessageVisible = false;
                            Navigator.popAndPushNamed(
                                context, MyHomePage.routeName);
                          }
                        });
                      } else {
                        setState(() {
                          IsMessageVisible = true;
                          message = "Šifre se ne poklapaju!";
                        });
                      }
                    } else {
                      setState(() {
                        IsMessageVisible = true;
                        message = "Šifre se ne poklapaju!";
                      });
                    }
                    // if (_firstName.text.trim().isEmpty ||
                    //     _lastName.text.trim().isEmpty ||
                    //     _phoneNumber.text.trim().isEmpty ||
                    //     _email.text.trim().isEmpty ||
                    //     _userName.text.trim().isEmpty ||
                    //     _password.text.trim().isEmpty ||
                    //     _repeatPassword.text.trim().isEmpty) {
                    //   setState(() {
                    //     IsMessageVisible = true;
                    //     message = "Niste ukucali sva polja!";
                    //   });
                    // } else if (_password.text != _repeatPassword.text) {
                    //   setState(() {
                    //     IsMessageVisible = true;
                    //     message = "Šifre se ne poklapaju!";
                    //   });
                    // } else {
                    //   setState(() {
                    //     IsMessageVisible = false;
                    //     message = "";
                    //   });
                    //   try {
                    //     baseProvider.setEndPoint("/api/User");
                    //     final request = {
                    //       "firstName": _firstName.text,
                    //       "lastName": _lastName.text,
                    //       "phoneNumber": _phoneNumber.text,
                    //       "userName": _userName.text,
                    //       "email": _email.text,
                    //       "password": _password.text,
                    //       "gender": selected.index + 1,
                    //       "userRoles": ["CA39052B-C121-4C58-189D-08DA6E6E173C"]
                    //     };
                    //     var tmpData =
                    //         await baseProvider.insertWithoutToken(request);
                    //     print(tmpData);
                    //     if (tmpData['status'].toString() == "200") {
                    //       Navigator.pushNamed(context, MyHomePage.routeName);
                    //     }
                    //     clearForm();
                    //   } catch (error) {
                    //     IsMessageVisible = true;
                    //     message = error.toString();
                    //   }
                    // }
                  },
                  child: Text(
                    ButtonMessage,
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
