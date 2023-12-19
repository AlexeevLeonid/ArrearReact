import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render() {
    return (
        <header>
            <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
                <NavbarBrand tag={Link} to="/">ArrearReact</NavbarBrand>
                <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
                    <ul className="navbar-nav flex-grow">
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/">Shop</NavLink>
                        </NavItem>
                        
                        {!this.props.user ? <>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/registration">Register</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} className="text-dark" to="/login">Login</NavLink>
                            </NavItem>
                        </> : <>
                                {this.props.user.userType != "Customer" ? <>
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/take">Get to work</NavLink>
                                </NavItem>
                                <NavItem>
                                        <NavLink tag={Link} className="text-dark" to="/done">Done work: {this.props.user.seps.length}</NavLink>
                                    </NavItem>
                                </> : <>
                                    <NavItem>
                                            <NavLink tag={Link} className="text-dark" to="/basket">Cart : {this.props.basket.length}</NavLink>
                                        </NavItem>
                                    </>
                                }
                                <li className="nav-item">
                                    <a className="nav-link disabled" href="#">{this.props.user.name} </a>
                                </li>
                                <li className="nav-item">
                                    <a className="nav-link disabled" href="#">Role : {this.props.user.userType}</a>
                                </li>
                                <li className="nav-item">
                                    <button className="nav-link btn" onClick={() => { this.props.setUser(undefined); window.location = "" } } >Leave</button>
                                </li>
                            </>
                        }
                    </ul>
                </Collapse>
            </Navbar>
        </header>
    );
  }
}
