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
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Sex</th>
                        <th>Size</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {user.seps.map(sep =>
                        <tr key={sep.id}>
                            <td>{sep.sex}</td>
                            <td>{sep.size}</td>
                            <td>
                                <button className=
                                    {this.props.user && this.props.user.userType != "Customer" ?
                                        "btn btn-primary" : "btn btn-primary disabled"}
                                    onClick={this.props.user ? () => (this.postOrder(sep.id, this.props.user.token)) : () => { }}>
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
            : this.renderDefaultSepsTable(this.props.user);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
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

    async postOrder(id, token) {
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
