import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Country {
  final String title;
  final String id;

  Country({required this.title, required this.id});

  factory Country.fromJson(Map<String, dynamic> countriesjson) => Country(
        title: countriesjson["title"],
        id: countriesjson["id"],
      );
}
