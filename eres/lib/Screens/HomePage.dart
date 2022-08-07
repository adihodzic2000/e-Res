import 'package:eres/Screens/Company.dart';
import 'package:eres/Widgets/HeaderWidget.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:flutter/src/foundation/key.dart';
import 'package:flutter/src/widgets/framework.dart';
import 'package:provider/provider.dart';
import 'package:flutter_switch/flutter_switch.dart';

import '../Models/City.dart';
import '../Models/Country.dart';
import '../Providers/baseProvider.dart';
import '../Widgets/MyCustomForm.dart';

class HomePage extends StatefulWidget {
  static const String routeName = "/home";
  final bool IsVisibleFiltering;

  const HomePage({Key? key, required this.IsVisibleFiltering})
      : super(key: key);

  @override
  State<HomePage> createState() => _HomePageState();
}

class _HomePageState extends State<HomePage> {
  List<dynamic> companies = [];
  List<Country> countries = [];
  List<City> cities = [];

  late BaseProvider _baseProvider;
  Country? selectedCountry;
  City? selectedCity;

  bool isHotel = true;
  bool isApartment = true;
  bool isPressedButtonHide = false;

  TextEditingController _minPrice = new TextEditingController();
  TextEditingController _maxPrice = new TextEditingController();

  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    _minPrice.text = "0";
    _maxPrice.text = "100";
    loadCountries();
    super.initState();
  }

  Future loadData() async {
    _baseProvider.setEndPoint(
        "/api/Company/get-company/${selectedCountry?.id}/${selectedCity?.id}/" +
            isApartment.toString() +
            "/" +
            isHotel.toString());
    var tmpData = await _baseProvider.get();
    setState(() {
      companies = tmpData['data'];
    });
  }

  Future loadCountries() async {
    _baseProvider.setEndPoint("/api/Country/get-countries");
    var tmpData = await _baseProvider.get();
    setState(() {
      countries.clear();
      countries.add(Country(
          title: "SVE DRŽAVE", id: "00000000-0000-0000-0000-000000000000"));
      countries.addAll(tmpData['data']
          .map((x) => Country.fromJson(x))
          .cast<Country>()
          .toList());
      selectedCountry = countries[0];
    });
    loadCities();
  }

  Future loadCities() async {
    _baseProvider.setEndPoint(
        "/api/City/get-cities-by-country-id/${selectedCountry?.id}");
    var tmpData = await _baseProvider.get();
    setState(() {
      cities.clear();
      cities.add(City(
          title: "SVI GRADOVI", id: "00000000-0000-0000-0000-000000000000"));

      cities.addAll(
          tmpData['data'].map((x) => City.fromJson(x)).cast<City>().toList());
      if (cities.length > 0) {
        selectedCity = cities[0];
      }
    });
    loadData();
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
                Visibility(
                    visible: widget.IsVisibleFiltering && !isPressedButtonHide,
                    child: Center(
                        child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceAround,
                      children: [
                        DropdownButton<Country>(
                          value: selectedCountry,
                          dropdownColor: Colors.black,
                          onChanged: (Country? newValue) {
                            setState(() {
                              selectedCountry = newValue!;
                              loadCities();
                            });
                          },
                          items: countries.map((Country country) {
                            return new DropdownMenuItem<Country>(
                              value: country,
                              child: new Text(
                                country.title,
                                style: new TextStyle(color: Colors.white),
                              ),
                            );
                          }).toList(),
                        ),
                        DropdownButton<City>(
                            value: selectedCity,
                            dropdownColor: Colors.black,
                            onChanged: (City? newValue) {
                              setState(() {
                                selectedCity = newValue!;
                              });
                            },
                            items: cities.map((City city) {
                              return new DropdownMenuItem<City>(
                                value: city,
                                child: new Text(
                                  city.title,
                                  style: new TextStyle(color: Colors.white),
                                ),
                              );
                            }).toList())
                      ],
                    ))),
                // Visibility(
                //   visible: widget.IsVisibleFiltering && !isPressedButtonHide,
                //   child: Container(
                //     margin: EdgeInsets.all(10),
                //     child: MyCustomForm("Minimalna cijena", _minPrice, true,
                //         TextInputType.number,
                //         IsObsecured: false),
                //   ),
                // ),
                // Visibility(
                //   visible: widget.IsVisibleFiltering && !isPressedButtonHide,
                //   child: Container(
                //     margin: EdgeInsets.all(10),
                //     child: MyCustomForm("Maksimalna cijena", _maxPrice, true,
                //         TextInputType.number,
                //         IsObsecured: false),
                //   ),
                // ),
                Visibility(
                  visible: widget.IsVisibleFiltering && !isPressedButtonHide,
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      ElevatedButton(
                        style: ButtonStyle(
                          backgroundColor:
                              MaterialStateProperty.all(Colors.green),
                          shape:
                              MaterialStateProperty.all<RoundedRectangleBorder>(
                            RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(18.0),
                              side: BorderSide(
                                color: Colors.teal,
                                width: 0.5,
                              ),
                            ),
                          ),
                        ),
                        child: Text('POTVRDI'),
                        onPressed: () {
                          loadData();
                        },
                      ),
                      ElevatedButton(
                        style: ButtonStyle(
                          backgroundColor:
                              MaterialStateProperty.all(Colors.grey),
                          shape:
                              MaterialStateProperty.all<RoundedRectangleBorder>(
                            RoundedRectangleBorder(
                              borderRadius: BorderRadius.circular(18.0),
                              side: BorderSide(
                                color: Colors.grey,
                                width: 0.5,
                              ),
                            ),
                          ),
                        ),
                        child: Text('Sakrij filtere'),
                        onPressed: () {
                          setState(() {
                            isPressedButtonHide = true;
                          });
                        },
                      ),
                    ],
                  ),
                ),
                Row(
                  children: [
                    SizedBox(
                      width: 10,
                    ),
                    Text("PREPORUČENO",
                        style: TextStyle(color: Colors.white, fontSize: 20)),
                    Switch(
                      value: isHotel,
                      onChanged: (bool isOn) {
                        setState(() {
                          isHotel = isOn;
                          loadData();
                        });
                      },
                      activeColor: Colors.green,
                      inactiveTrackColor: Colors.grey,
                      inactiveThumbColor: Colors.grey,
                    ),
                    Text(
                      "Hoteli",
                      style: TextStyle(color: Colors.white),
                    ),
                    Switch(
                      value: isApartment,
                      onChanged: (bool isOn) {
                        setState(() {
                          isApartment = isOn;
                          loadData();
                        });
                      },
                      activeColor: Colors.green,
                      inactiveTrackColor: Colors.grey,
                      inactiveThumbColor: Colors.grey,
                    ),
                    Text(
                      "Apartmani",
                      style: TextStyle(color: Colors.white),
                    )
                  ],
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
