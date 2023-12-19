import React, { Component } from 'react';

export class GetToWork extends Component {
    static displayName = GetToWork.name;

    constructor(props) {
        super(props);
        this.state = { sepuls: [], loading: true };
        this.renderDefaultSepsTable = this.renderDefaultSepsTable.bind(this);
    }

    componentDidMount() {
        this.getSepsToWork(this.props.user.token);
    }



    renderDefaultSepsTable(seps) {
        return (
            <table className='table table-hover' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th scope="col">Id</th>
                        <th scope="col">Sex</th>
                        <th scope="col">Size</th>
                        <th scope="col">Adress</th>
                        <th scope="col"></th>
                    </tr>
                </thead>
                <tbody>
                    {seps.map(sep =>
                        <tr key={sep.id}>
                            <td>{sep.id}</td>
                            <td>{sep.sex}</td>
                            <td>{sep.size}</td>
                            <td>{sep.deliveryAdress}</td>
                            <td>
                                <button className=
                                    {this.props.user && this.props.user.userType != "Customer" ?
                                        "btn btn-primary" : "btn btn-primary disabled"}
                                    onClick={this.props.user ? () => (this.takeInWork(sep.id, this.props.user.token)) : () => { }}>
                                    Take
                                </button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading......</em></p>
            : this.renderDefaultSepsTable(this.state.sepuls);

        return (
            <div>
                <h1 id="tabelLabel" >Sepulkas that need to be taken to work</h1>
                <p>Hire the incoming sepulkas from the list</p>
                {contents}
            </div>
        );
    }

    async getSepsToWork(token) {
        const response = await fetch('sep/towork', {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${token}`
            },
        });
        const data = (await response.json()).map(sep =>
            sep.sex == 0 ?
                { ...sep, sex: "Male" }
                : { ...sep, sex: "Female" })
        this.setState({ sepuls: data, loading: false });
    }

    async takeInWork(id, token) {
        var details = id
        const response = await fetch('sep/take', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(details)
        });
        this.componentDidMount()
    }
}
