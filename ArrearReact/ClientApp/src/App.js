import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { SepsOrder, FetchData } from './components/SepsOrder';
import { Home } from './components/Home';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Registration } from './components/Registration';
import { GetToWork } from './components/GetToWork';
import { WorkDone } from './components/WorkDone';
import './custom.css';
import { Basket } from './components/Basket';
/*import AppRoutes from './AppRoutes';*/

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { user: undefined, basket: [] };
        this.setUser = this.setUser.bind(this);
        this.setBasket = this.setBasket.bind(this);
        this.updateUser = this.updateUser.bind(this);
        this.appRoutes = this.appRoutes.bind(this);
    }

    setBasket(value) {
        this.setState({ basket: value });
    }

    setUser(value) {
        this.setState({ user: value });
    }

    async updateUser() {
        const token = this.state.user.token
        const response = await fetch('user', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${token}`
            },
        });
        const data = await response.json();
        if (data.userType == 0) data.userType = "Customer"
        else if (data.userType == 1) data.userType = "ManagerFirstWorkshop"
        else if (data.userType == 2) data.userType = "ManagerSecondWorkshop"
        data.seps = data.seps.map(sep =>
            sep.sex == 0 ?
                { ...sep, sex: "Male" }
                : { ...sep, sex: "Female" })
        this.setUser(data)
    }

    appRoutes() {
        return [
            {
                index: true,
                element: <SepsOrder user={this.state.user} basket={this.state.basket} setBasket={this.setBasket} />
            },
            {
                path: '/basket',
                element: <Basket user={this.state.user} basket={this.state.basket} setBasket={this.setBasket} />
            },
            {
                path: '/registration',
                element: <Registration setUser={this.setUser} />
            },
            {
                path: '/login',
                element: <Login setUser={this.setUser} />
            },
            {
                path: '/take',
                element: <GetToWork user={this.state.user} />
            },
            {
                path: '/done',
                element: <WorkDone user={this.state.user} updateUser={this.updateUser} />
            }
        ];
    }



  render() {
    return (
        <Layout user={this.state.user} setUser={this.setUser} basket={this.state.basket }>
        <Routes>
          {this.appRoutes().map((route, index) => {
            const { element, ...rest } = route;
            return <Route key={index} {...rest} element={element} />;
          })}
        </Routes>
      </Layout>
    );
  }
}
