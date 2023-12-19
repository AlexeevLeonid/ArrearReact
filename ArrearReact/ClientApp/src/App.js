import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';
import { DefaultSeps, FetchData } from './components/FetchData';
import { Home } from './components/Home';
import { Layout } from './components/Layout';
import { Login } from './components/Login';
import { Registration } from './components/Registration';
import { TakeInWork } from './components/TakeInWork';
import { WorkDone } from './components/WorkDone';
import './custom.css';
/*import AppRoutes from './AppRoutes';*/

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = { user: undefined };
        this.setUser = this.setUser.bind(this);
        this.updateUser = this.updateUser.bind(this);
        this.appRoutes = this.appRoutes.bind(this);
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
        const value = await response.json();
        if (value.userType == 0) value.userType = "Customer"
        else if (value.userType == 1) value.userType = "ManagerFirstWorkshop"
        else if (value.userType == 2) value.userType = "ManagerSecondWorkshop"
        this.setUser(value)
    }

    appRoutes() {
        return [
            {
                index: true,
                element: <Home />
            },
            {
                path: '/fetch-data',
                element: <DefaultSeps user={this.state.user} />
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
                element: <TakeInWork user={this.state.user} />
            },
            {
                path: '/done',
                element: <WorkDone user={this.state.user} updateUser={this.updateUser} />
            }
        ];
    }



  render() {
    return (
        <Layout user={this.state.user} setUser={this.setUser } >
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
