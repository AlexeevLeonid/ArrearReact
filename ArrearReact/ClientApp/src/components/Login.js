import React, { Component } from 'react';

export class Login extends Component {
    static displayName = Login.name;

    constructor(props) {
        super(props);
        this.state = { username: "", password: ""};

        this.changeUsername = this.changeUsername.bind(this);
        this.changePassword = this.changePassword.bind(this);
        this.postForm = this.postForm.bind(this);
    }

    changeUsername(event) {
        this.setState({ username: event.target.value });
    }

    changePassword(event) {
        this.setState({ password: event.target.value });
    }

    async postForm() {
        const user = {
            Name: this.state.username,
            Password: this.state.password,
        }
        const response = await fetch('authenticate', {
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
                        <ul className="list-group">
                            <button type="button" className="btn btn-primary" onClick={this.postForm}>
                                Login
                            </button>
                        </ul>
                    </li>
                </div>
            </div>
        );
    }
}
