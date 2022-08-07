import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class MyCustomFormText extends StatefulWidget {
  final String s;
  final TextEditingController text;

  final bool IsEnabled;
  final bool IsObsecured;
  const MyCustomFormText(this.s, this.text, this.IsEnabled,
      {super.key, required this.IsObsecured});

  @override
  MyCustomFormState createState() {
    return MyCustomFormState();
  }
}

class MyCustomFormState extends State<MyCustomFormText> {
  final _formKey = GlobalKey<FormState>();

  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    // Build a Form widget using the _formKey created above.
    return Form(
      key: _formKey,
      child: Container(
        padding: EdgeInsets.fromLTRB(10, 20, 10, 0),
        child: TextFormField(
          style: TextStyle(color: Colors.white),
          obscureText: widget.IsObsecured,
          enabled: widget.IsEnabled,
          controller: widget.text,
          decoration: InputDecoration(
            label: Text(
              widget.s,
              style: TextStyle(color: Colors.white),
            ),
            labelStyle: TextStyle(color: Colors.white),
            hintText: widget.s,
            hintStyle: TextStyle(color: Colors.grey),
            enabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.white,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: Colors.white,
              ),
            ),
          ),
          validator: (value) {
            if (widget.IsObsecured &&
                (value == null && value == null ? false : value.length < 6))
              return "Molimo upišite 6 znakova";
            if (value == null || value.isEmpty) {
              return 'Molimo upišite navedeno polje';
            }
            return null;
          },
          onChanged: (value) {
            if (_formKey.currentState!.validate()) {
              ScaffoldMessenger.of(context);
            }
          },
        ),
      ),
    );
  }
}
