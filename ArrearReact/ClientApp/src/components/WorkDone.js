import React, { Component } from 'react';

export class WorkDone extends Component {
    static displayName = WorkDone.name;

    constructor(props) {
        super(props);
        this.state = { sepuls: [], loading: true };
        this.renderDefaultSepsTable = this.renderDefaultSepsTable.bind(this);
    }

    componentDidMount() {
        this.getSepsToWork(this.props.user);
    }



    renderDefaultSepsTable(user) {
        return (
            <div className='container row'>
                <table className='table table-hover col-6' aria-labelledby="tabelLabel">
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
                        {user.seps.map(sep =>
                            <tr key={sep.id}>
                                <td>{sep.id}</td>
                                <td>{sep.sex}</td>
                                <td>{sep.size}</td>
                                <td>{sep.deliveryAdress}</td>
                                <td>
                                    <button className=
                                        {this.props.user && this.props.user.userType != "Customer" ?
                                            "btn btn-primary" : "btn btn-primary disabled"}
                                        onClick={this.props.user ? () => (this.doneWork(sep.id, this.props.user.token)) : () => { }}>
                                        Take
                                    </button>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading......</em></p>
            : this.renderDefaultSepsTable(this.props.user);

        return (
            <div>
                <h1 id="tabelLabel" >Completing of the work</h1>
                <p>Click "Done" after completing the work and further sending.</p>
                {contents}
            </div>
        );
    }

    async getSepsToWork(user) {
        if (this.props.user) {
            this.props.updateUser()
            const data = this.props.user.seps
            this.setState({ sepuls: data, loading: false });
        }
    }

    async doneWork(id, token) {
        var details = id
        const response = await fetch('sep/done', {
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
