import React, { Component } from 'react';

export class Registration extends Component {
    static displayName = Registration.name;

    constructor(props) {
        super(props);
        this.state = { username: "", password: "", role: "Customer", dropdown: false };
        this.changeUsername = this.changeUsername.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.toggleOpen = this.toggleOpen.bind(this);
        this.changeRole = this.changeRole.bind(this);
        this.postForm = this.postForm.bind(this);
    }


    changeUsername(event) {
        this.setState({ username: event.target.value });
    }

    changePassword(event) {
        this.setState({ password: event.target.value });
    }

    changeRole(irole) {
        this.setState({ role: irole });
    }

    toggleOpen() {
        this.setState({ dropdown: !this.state.dropdown });
    }

    async postForm() {
        const user = {
            Name: this.state.username,
            Password: this.state.password,
            UserType: this.state.role,
        }
        const response = await fetch('register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(user)
        });
        const data = await response.json();
        if (data.userType == 0) data.userType = "Customer"
        else if (data.userType == 1) data.userType = "ManagerFirstWorkshop"
        else if (data.userType == 2) data.userType = "ManagerSecondWorkshop"
        this.props.setUser(data)
    }

    render() {
        return (
            <div className="container row">
                <div className="col-4">
                <li className="list-group">
                    <ul className="list-group m-1">
                        <input className="form-control" type="text" aria-describedby="validationServer" placeholder="login" value={this.state.username} onChange={this.changeUsername} />
                    </ul>
                    <ul className="list-group m-1">
                        <input className="form-control" type="password" placeholder="password" value={this.state.password} onChange={this.changePassword} />
                    </ul>
                    <ul className="list-group m-1">
                        <div type="button" className="dropdown">
                            <button type="button" className="btn btn-primary" onClick={this.toggleOpen}>
                                {this.state.role}
                            </button>
                            <div
                                className={`dropdown-menu ${this.state.dropdown ? 'show' : ''}`}
                                aria-labelledby="dropdownMenuButton">
                                <button class="dropdown-item" onClick={() => { this.toggleOpen(); this.changeRole("Customer")}}>Customer</button>
                                <button class="dropdown-item" onClick={() => { this.toggleOpen(); this.changeRole("ManagerFirstWorkshop") }}>ManagerFirsWorkshop</button>
                                    <button class="dropdown-item" onClick={() => { this.toggleOpen(); this.changeRole("ManagerSecondWorkshop") }}>ManagerSecondWorkshop</button>
                            </div>
                        </div>
                    </ul>
                    <ul className="list-group">
                        <button type="button" className="btn btn-primary" onClick={this.postForm}>
                            Register
                        </button>
                    </ul>
                    </li>
                </div>
            </div>
        );
    }
}
