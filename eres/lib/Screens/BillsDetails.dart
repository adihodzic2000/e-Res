import 'dart:convert';
import 'package:eres/Providers/baseProvider.dart';
import 'package:eres/Screens/Bills.dart';
import 'package:eres/Widgets/HeaderWidget.dart';
import 'package:eres/Widgets/MyCustomForm.dart';
import 'package:eres/Widgets/MyCustomFormText.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:provider/provider.dart';
import 'package:intl/intl.dart';
import 'package:http/http.dart' as http;
import 'package:flutter_stripe/flutter_stripe.dart';
import 'dart:math';

class BillsDetails extends StatefulWidget {
  final String Id;
  final String TotalAmount;
  final String CompanyId;
  final String CompanyName;
  final String Paid;
  const BillsDetails(
      {Key? key,
      required this.Id,
      required this.TotalAmount,
      required this.CompanyName,
      required this.Paid,
      required this.CompanyId})
      : super(key: key);

  @override
  State<BillsDetails> createState() => _BillsDetailsState();
}

TextEditingController grade = new TextEditingController();
var formatter = NumberFormat("###.00", "en_US");

class _BillsDetailsState extends State<BillsDetails> {
  List<dynamic> items = [];
  late BaseProvider _baseProvider;
  String message = "Loading...";
  bool paid = false;
  @override
  void initState() {
    _baseProvider = context.read<BaseProvider>();
    loadData();
    paid = widget.Paid == "false" ? true : false;
    super.initState();
  }

  Future loadData() async {
    _baseProvider.setEndPoint("/api/Bills/get-bill-details/${widget.Id}");
    var tmpData = await _baseProvider.get();
    setState(() {
      items = tmpData['data'];
    });
  }

  Future payBill() async {
    _baseProvider.setEndPoint("/api/Bills/pay-bill");
    try {
      var tmpData = await _baseProvider.insert({'id': widget.Id});
      setState(() {
        paid = true;
      });
    } catch (error) {}
  }

  Map<String, dynamic>? paymentIntentData;

  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
        Id: 0,
        child: Column(
          children: [
            Container(
              margin: EdgeInsets.all(10),
              child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      widget.CompanyName,
                      style: TextStyle(color: Colors.white, fontSize: 25),
                    ),
                    Text(
                      widget.TotalAmount + "KM",
                      style: TextStyle(
                          color: widget.Paid == "false"
                              ? Colors.red
                              : Colors.green,
                          fontSize: 25),
                    )
                  ]),
            ),
            Container(
                margin: EdgeInsets.all(10),
                child: SingleChildScrollView(
                  child: Row(
                    crossAxisAlignment: CrossAxisAlignment.center,
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      SingleChildScrollView(
                        scrollDirection: Axis.horizontal,
                        child: _createDataTable(),
                      ),
                    ],
                  ),
                )),
            Visibility(
              visible: paid,
              child: Container(
                  margin: EdgeInsets.all(10),
                  width: double.infinity,
                  decoration: BoxDecoration(
                    borderRadius: BorderRadius.circular(10),
                    color: Colors.green,
                  ),
                  child: TextButton(
                    onPressed: () async {
                      await makePayment();
                    },
                    child: Text("PLATI"),
                  )),
            ),
          ],
        ));
  }

  DataTable _createDataTable() {
    return DataTable(
        headingRowColor: MaterialStateColor.resolveWith(
            (states) => const Color.fromARGB(255, 54, 71, 79)),
        headingTextStyle: const TextStyle(color: Colors.white),
        columnSpacing: 10,
        decoration: const BoxDecoration(
            border: Border(
                top: BorderSide(width: 1, color: Colors.grey),
                left: BorderSide(width: 1, color: Colors.grey),
                bottom: BorderSide(width: 1, color: Colors.grey),
                right: BorderSide(width: 1, color: Colors.grey))),
        showCheckboxColumn: false,
        columns: _createColumns(),
        rows: _createRows());
  }

  List<DataColumn> _createColumns() {
    return [
      DataColumn(
          label: Expanded(
        child: const Text(
          'Naziv',
          softWrap: true,
          textAlign: TextAlign.center,
        ),
      )),
      DataColumn(
          label: Expanded(
        child: const Text(
          'Količina',
          textAlign: TextAlign.center,
          softWrap: true,
        ),
      )),
      DataColumn(
          label: Expanded(
              child: const Text(
        'Cijena',
        softWrap: true,
        textAlign: TextAlign.center,
      ))),
      DataColumn(
          label: Expanded(
              child: const Text(
        'Ukupna cijena',
        softWrap: true,
        textAlign: TextAlign.center,
      ))),
    ];
  }

  List<DataRow> _createRows() {
    return items
        .map((x) => DataRow(
              cells: [
                DataCell(Text(
                  x['name'],
                  softWrap: true,
                  textAlign: TextAlign.center,
                  style: TextStyle(color: Colors.white),
                )),
                DataCell(Center(
                  child: Text(x['quantity'].toString(),
                      style: TextStyle(color: Colors.white),
                      softWrap: true,
                      textAlign: TextAlign.center),
                )),
                DataCell(Center(
                  child: Text(formatter.format(x['price']).toString(),
                      style: TextStyle(color: Colors.white),
                      softWrap: true,
                      textAlign: TextAlign.center),
                )),
                DataCell(Center(
                  child: Text(formatter.format(x['totalAmount']).toString(),
                      style: TextStyle(color: Colors.white),
                      softWrap: true,
                      textAlign: TextAlign.center),
                )),
              ],
            ))
        .toList();
  }

  Future<void> makePayment() async {
    try {
      print(widget.TotalAmount);
      paymentIntentData = await createPaymentIntent(
          (double.parse(widget.TotalAmount) * 100).round().toString(),
          'bam'); //json.decode(response.body);
      // print('Response body==>${response.body.toString()}');
      await Stripe.instance
          .initPaymentSheet(
              paymentSheetParameters: SetupPaymentSheetParameters(
                  paymentIntentClientSecret:
                      paymentIntentData!['client_secret'],
                  style: ThemeMode.dark,
                  merchantDisplayName: 'ERES'))
          .then((value) {});

      ///now finally display payment sheeet
      displayPaymentSheet();
    } catch (e, s) {}
  }

  displayPaymentSheet() async {
    try {
      await Stripe.instance
          .presentPaymentSheet(
              parameters: PresentPaymentSheetParameters(
        clientSecret: paymentIntentData!['client_secret'],
        confirmPayment: true,
      ))
          .then((newValue) {
        ScaffoldMessenger.of(context)
            .showSnackBar(SnackBar(content: Text("Uspješno plaćeno")));
        paymentIntentData = null;
      }).onError((error, stackTrace) {});
      await payBill();
      showAlertDialog(context);
    } on StripeException catch (e) {
      showDialog(
          context: context,
          builder: (_) => AlertDialog(
                content: Text("Otkazano..."),
              ));
    } catch (e) {}
  }

  //  Future<Map<String, dynamic>>
  createPaymentIntent(String amount, String currency) async {
    try {
      Map<String, dynamic> body = {
        'amount': amount,
        'currency': currency,
        'payment_method_types[]': 'card'
      };
      var response = await http.post(
          Uri.parse('https://api.stripe.com/v1/payment_intents'),
          body: body,
          headers: {
            'Authorization':
                'Bearer sk_test_51LS4cRFZdZe5rLR67vCn4AqcNBOp7933naZeUCdKREJuHzobqqdK7SfP719aZNPbcEguUyamBHE3Esu0YNv8tCCS007lySqC2s',
            'Content-Type': 'application/x-www-form-urlencoded'
          });
      return jsonDecode(response.body);
    } catch (err) {}
  }

  calculateAmount(String amount) {
    final a = (int.parse(amount)) * 100;
    return a.toString();
  }

  showAlertDialog(BuildContext context) {
    final _formKey = GlobalKey<FormState>();
    Widget cancelButton = TextButton(
      child: Text("Otkaži"),
      onPressed: () {
        Navigator.of(context).pop(); // dismiss dialog
      },
    );

    Widget continueButton = TextButton(
      child: Text("Nastavi"),
      onPressed: () async {
        if (_formKey.currentState!.validate()) {
          _baseProvider.setEndPoint("/api/Reservation/add-review");
          final request = {"grade": grade.text, "companyId": widget.CompanyId};
          var tmpData = await _baseProvider.insert(request);
          print(tmpData);
          Navigator.popAndPushNamed(context, Bills.routeName);
        }
      },
    );

    // set up the AlertDialog
    AlertDialog alert = AlertDialog(
      title: Text("Upišite ocjenu za objekat"),
      content: Form(
        key: _formKey,
        child: TextFormField(
          keyboardType: TextInputType.numberWithOptions(decimal: false),
          inputFormatters: [
            FilteringTextInputFormatter.allow(RegExp(r'^[Z0-9]+$')),
          ],
          style: TextStyle(color: Colors.black),
          controller: grade,
          decoration: InputDecoration(
            label: Text(
              "Ocjena 5-10",
              style: TextStyle(color: Colors.black),
            ),
            labelStyle: TextStyle(color: Colors.black),
            hintText: "Ocjena",
            hintStyle: TextStyle(color: Colors.grey),
            enabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.black,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.black,
              ),
            ),
            errorBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.red,
              ),
            ),
            focusedErrorBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.red,
              ),
            ),
          ),
          validator: (value) {
            if (value == null ||
                value.isEmpty ||
                int.parse(value) < 5 ||
                int.parse(value) > 10) {
              return 'Niste ukucali ispravno vrijednost';
            }
            return null;
          },
        ),
      ),
      actions: [
        cancelButton,
        continueButton,
      ],
    );

    // show the dialog
    showDialog(
      context: context,
      builder: (BuildContext context) {
        return alert;
      },
    );
  }
}
