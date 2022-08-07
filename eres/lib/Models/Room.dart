import 'package:json_annotation/json_annotation.dart';

@JsonSerializable()
class Room {
  final String title;
  final String id;

  Room({required this.title, required this.id});

  factory Room.fromJson(Map<String, dynamic> roomsJson) => Room(
        title: roomsJson["title"],
        id: roomsJson["id"],
      );
}
