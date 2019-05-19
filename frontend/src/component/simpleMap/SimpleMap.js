import React, { Component } from 'react';
import { Map, InfoWindow, Marker, GoogleApiWrapper } from 'google-maps-react';


class SimpleMap extends Component {

  state = {
    isCurrentLocation: true,
    markerCoords: this.props.center,
    showingInfoWindow: false,
    activeMarker: {},
    selectedPlace: {},
  };

  onMarkerClick = (props, marker, e) =>
    this.setState({
      selectedPlace: props,
      activeMarker: marker,
      showingInfoWindow: true
    });

  handleClick = (mapProps, map, event) => {
    this.setState(
      {
        isCurrentLocation: false,
        markerCoords: {
          lat: event.latLng.lat(),
          lng: event.latLng.lng()
        }
      }
    );
    this.props.setCoords(this.state.markerCoords, !this.props.isForm);
  };

  renderMarkers(markers) {
    if(!markers) return;
    return markers.map(marker =>
      <Marker
        key={marker.id}
        title={`${marker.title}`}
        name={`${marker.locationName}`}
        comment={`${marker.comment}`}
        position={{ lat: marker.latitude, lng: marker.longitude }}
        onClick={this.onMarkerClick}
        icon={{
          url: "http://maps.google.com/mapfiles/ms/icons/purple-dot.png"
        }}
      />
    );
  }

  render() {
    const marks = this.renderMarkers(this.props.points);
    return (
      <Map
        google={this.props.google}
        zoom={14}
        initialCenter={this.props.center}
        center={this.props.center}
        onClick={this.handleClick}
      >
        <Marker
          name={'Вы здесь!'}
          position={this.state.isCurrentLocation ? this.props.center : this.state.markerCoords}
          onClick={this.onMarkerClick}

        />

        {marks}

        <InfoWindow
          marker={this.state.activeMarker}
          visible={this.state.showingInfoWindow}>
          <div>
            <div>{this.state.selectedPlace.name}</div>
            <div>{this.state.selectedPlace.title}</div>
          </div>
        </InfoWindow>
      </Map>
    )
  }
}

export default GoogleApiWrapper({
  apiKey: "AIzaSyA7EstUQ0Un1VCB54jyhHHXwYfg8FyNOuY",
  language: "ru"
})(SimpleMap)