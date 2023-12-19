import React, { Component } from 'react';

export class DefaultSeps extends Component {
  static displayName = DefaultSeps.name;

  constructor(props) {
    super(props);
      this.state = { forecasts: [], loading: true };
      this.renderDefaultSepsTable = this.renderDefaultSepsTable.bind(this);
  }

  componentDidMount() {
    this.getDefaultSeps();
    }



    renderDefaultSepsTable(seps) {
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
                {seps.map(sep =>
                    <tr key={sep.id}>
                        <td>{sep.sex}</td>
                        <td>{sep.size}</td>
                        <td>
                            <button className=
                                {this.props.user && this.props.user.userType == "Customer" ?
                                    "btn btn-primary" : "btn btn-primary disabled"}
                                onClick={this.props.user ? () => (this.postOrder(sep.size, sep.sex, this.props.user.token)) : () => { } }>
                            Order
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
      : this.renderDefaultSepsTable(this.state.forecasts);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async getDefaultSeps() {
      const response = await fetch('sep/default');
    const data = await response.json();
    this.setState({ forecasts: data, loading: false });
    }

    async postOrder(size, sex, token) {
        var details =
        {
            Size : String(size),
            Sex: "Male",
            deliveryAdress : "aaa"
        }
        const response = await fetch('sep/order', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(details)
        });
        const data = await response.json();
    }
}
