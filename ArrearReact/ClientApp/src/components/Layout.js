import React, { Component } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';

export class Layout extends Component {
  static displayName = Layout.name;

    constructor(props) {
        super(props);

    }

  render() {
    return (
      <div>
            <NavMenu user={this.props.user} setUser={this.props.setUser} basket={this.props.basket} />
        <Container>
          {this.props.children}
        </Container>
      </div>
    );
  }
}
