import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class City {
  final String title;
  final String id;

  City({required this.title, required this.id});

  factory City.fromJson(Map<String, dynamic> citiesJson) => City(
        title: citiesJson["title"],
        id: citiesJson["id"],
      );
}
