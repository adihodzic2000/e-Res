import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';
import 'package:latlong2/latlong.dart';
import '../Widgets/HeaderWidget.dart';
import 'package:flutter/material.dart';
import 'package:flutter_map/flutter_map.dart';

class Maps extends StatefulWidget {
  static const String routeName = "/maps";
  const Maps({Key? key}) : super(key: key);

  @override
  State<Maps> createState() => _MapsState();
}

class _MapsState extends State<Maps> with TickerProviderStateMixin {
  @override
  void initState() {
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return HeaderWidget(
        Id: 0,
        child: Container(
          width: double.infinity,
          height: 600,
          child: FlutterMap(
            options: MapOptions(
              center: LatLng(43.3438, 17.8078),
              maxZoom: 18.0,
            ),
            layers: [
              TileLayerOptions(
                  urlTemplate:
                      'https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png',
                  subdomains: ['a', 'b', 'c']),
              new MarkerLayerOptions(markers: [
                Marker(
                    point: LatLng(43.3438, 17.8078),
                    width: 10,
                    height: 10,
                    builder: (context) => new Container(
                          child: Icon(
                            Icons.location_on,
                            color: Colors.blue,
                          ),
                        ))
              ]),
            ],
          ),
        ));
  }
}
