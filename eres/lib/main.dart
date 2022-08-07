import 'dart:io';
import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Screens/Chat.dart';
import 'package:eres/Screens/HomePage.dart';
import 'package:flutter/material.dart';
import 'package:loading_animation_widget/loading_animation_widget.dart';
import 'package:provider/provider.dart';

import 'Screens/Company.dart';
import 'Screens/Maps.dart';

void main() {
  HttpOverrides.global = new MyHttpOverrides();

  runApp(MyApp());
}

class CompanyColors {
  CompanyColors._(); // this basically makes it so you can instantiate this class

  static MaterialColor blue = MaterialColor(
    _bluePrimaryValue,
    <int, Color>{
      50: Color(0xFFE3F2FD),
      100: Color(0xFFBBDEFB),
      200: Color(0xFF90CAF9),
      300: Color(0xFF64B5F6),
      400: Color(0xFF42A5F5),
      500: Color(_bluePrimaryValue),
      600: Color(0xFF1E88E5),
      700: Color(0xFF1976D2),
      800: Color(0xFF1565C0),
      900: Color(0xFF0D47A1),
    },
  );
  static int _bluePrimaryValue = 0xFF003a52;
}

bool IsPassing = false;
String Message = " ";

class MyApp extends StatelessWidget {
  MyApp({Key? key}) : super(key: key);

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => BaseProvider()),
      ],
      child: MaterialApp(
        debugShowCheckedModeBanner: false,
        title: 'Flutter Demo',
        theme: ThemeData(
            primarySwatch: CompanyColors.blue,
            scaffoldBackgroundColor: CompanyColors.blue),
        home: MyHomePage(title: 'e-Res'),
        onGenerateRoute: (settings) {
          if (settings.name == HomePage.routeName) {
            return MaterialPageRoute(
                builder: ((context) => HomePage(
                      IsVisibleFiltering: false,
                    )));
          } else if (settings.name == Maps.routeName) {
            return MaterialPageRoute(builder: ((context) => const Maps()));
          }

          // else if (settings.name == FilterPage.routeName) {
          //   return MaterialPageRoute(builder: ((context) => FilterPage()));
          // }

          return null;
        },
      ),
    );
  }
}

class MyHomePage extends StatefulWidget {
  MyHomePage({Key? key, required this.title}) : super(key: key);
  static String routeName = "/login";
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class MyHttpOverrides extends HttpOverrides {
  @override
  HttpClient createHttpClient(SecurityContext? context) {
    return super.createHttpClient(context)
      ..badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
  }
}

class _MyHomePageState extends State<MyHomePage> {
  TextEditingController _usernameController = TextEditingController();
  TextEditingController _passwordController = TextEditingController();
  late BaseProvider _baseProvider;

  @override
  Widget build(BuildContext context) {
    _baseProvider = Provider.of<BaseProvider>(context, listen: false);

    return SafeArea(
        child: Scaffold(
            appBar: AppBar(
              title: Row(
                mainAxisAlignment: MainAxisAlignment.start,
                children: [Text("ERES")],
              ),
            ),
            body: Center(
              child: SingleChildScrollView(
                  child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                crossAxisAlignment: CrossAxisAlignment.center,
                children: [
                  Container(
                    margin: EdgeInsets.all(20),
                    height: 500,
                    width: double.infinity,
                    decoration: BoxDecoration(
                        border: Border(
                            bottom: BorderSide(
                                color: Color.fromARGB(255, 74, 67, 177)),
                            left: BorderSide(
                                color: Color.fromARGB(255, 74, 67, 177)),
                            right: BorderSide(
                                color: Color.fromARGB(255, 74, 67, 177)),
                            top: BorderSide(
                                color: Color.fromARGB(255, 74, 67, 177))),
                        color: Color.fromARGB(255, 0, 161, 226)),
                    child: Column(
                      mainAxisAlignment: MainAxisAlignment.start,
                      children: [
                        SizedBox(
                          height: 10,
                        ),
                        Container(
                            height: 70,
                            width: 100,
                            child: Icon(
                              Icons.verified_user,
                              size: 80,
                              color: Colors.white,
                            )),
                        SizedBox(height: 30),
                        Text("Prijava korisnika",
                            style:
                                TextStyle(fontSize: 12, color: Colors.white)),
                        Text(
                          "Unesite svoje podatke za prijavu",
                          style: TextStyle(fontSize: 12, color: Colors.white),
                        ),
                        Container(
                          margin: EdgeInsets.fromLTRB(30, 20, 30, 10),
                          child: TextField(
                            style: TextStyle(color: Colors.white),
                            controller: _usernameController,
                            decoration: InputDecoration(
                              hintText: "Korisničko ime",
                              hintStyle: TextStyle(color: Colors.white),
                              prefixIcon: Icon(
                                Icons.person,
                                color: Colors.white,
                              ),
                              border: OutlineInputBorder(
                                borderSide: BorderSide(
                                  color: Colors.white,
                                ),
                              ),
                            ),
                          ),
                        ),
                        Container(
                          margin: EdgeInsets.fromLTRB(30, 0, 30, 0),
                          child: TextField(
                            obscureText: true,
                            style: TextStyle(color: Colors.white),
                            controller: _passwordController,
                            decoration: InputDecoration(
                                hintText: "Lozinka",
                                hintStyle: TextStyle(color: Colors.white),
                                prefixIcon:
                                    Icon(Icons.lock, color: Colors.white),
                                border: OutlineInputBorder(
                                    borderRadius: BorderRadius.circular(4),
                                    borderSide:
                                        BorderSide(color: Colors.grey))),
                          ),
                        ),
                        SizedBox(
                          height: 5,
                        ),
                        Text(
                          Message,
                          style: TextStyle(
                              color: Color.fromARGB(255, 255, 94, 29)),
                        ),
                        Container(
                          margin: EdgeInsets.fromLTRB(30, 20, 30, 0),
                          width: double.infinity,
                          height: 50,
                          decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(5),
                              color: Color.fromARGB(255, 0, 58, 82)),
                          child: InkWell(
                            onTap: () async {
                              setState(() {
                                IsPassing = true;
                              });

                              try {
                                final auth = {
                                  'username': _usernameController.text,
                                  'password': _passwordController.text,
                                };

                                _baseProvider.setEndPoint("/api/Auth");
                                var response = await _baseProvider.LogIn(auth);

                                if (response != null) {
                                  Message = "";
                                  Navigator.popAndPushNamed(
                                      context, HomePage.routeName);
                                } else
                                  Message =
                                      "Pogriješili ste korisničko ime ili lozinku";
                              } catch (e) {}
                              if (mounted) {
                                setState(() {
                                  IsPassing = false;
                                });
                              }
                            },
                            child: Row(
                                mainAxisAlignment: MainAxisAlignment.center,
                                children: [
                                  Container(
                                      child: Text(
                                    "Prijava",
                                    style: TextStyle(
                                        color: Colors.white,
                                        fontWeight: FontWeight.bold,
                                        fontSize: 15),
                                  )),
                                  Icon(
                                    Icons.arrow_circle_right_outlined,
                                    color: Colors.white,
                                  )
                                ]),
                          ),
                        ),
                        TextButton(
                            onPressed: () {},
                            child: Text(
                              "Zaboravili ste šifru?",
                              style: TextStyle(color: Colors.white),
                            )),
                        SizedBox(
                          height: 5,
                        ),
                        Expanded(
                            child: Visibility(
                                visible: IsPassing,
                                child: Center(
                                  child:
                                      LoadingAnimationWidget.staggeredDotsWave(
                                    color: Color.fromARGB(255, 255, 255, 255),
                                    size: 50,
                                  ),
                                )))
                      ],
                    ),
                  ),
                ],
              )),
            )));
  }
}
