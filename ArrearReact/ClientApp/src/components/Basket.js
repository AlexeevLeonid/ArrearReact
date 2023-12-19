import React, { Component } from 'react';

export class Basket extends Component {
    static displayName = Basket.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], adress: "", loading: true };
        this.renderDefaultSepsTable = this.renderDefaultSepsTable.bind(this);
        this.changeAdress = this.changeAdress.bind(this);
        this.orderInBasket = this.orderOutBasket.bind(this);
        this.orderBasket = this.orderBasket.bind(this);
        this.getPrice = this.getPrice.bind(this);
    }


    changeAdress(event) {
        this.setState({ adress: event.target.value });
    }

    componentDidMount() {
        this.getDefaultSeps();
    }

    getPrice(sep) {
        var result = 5 * sep.Size
        if (sep.Sex == "Female") result = result * 2
        return result
    }



    renderDefaultSepsTable(seps) {
        return (<>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Sex</th>
                        <th>Size</th>
                        <th>Adress</th>
                        <th>Price</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {seps.map((sep, index) =>
                        <tr key={index}>
                            <td>{sep.Sex}</td>
                            <td>{sep.Size}</td>
                            <td>{sep.deliveryAdress}</td>
                            <td>{this.getPrice(sep)}</td>
                            <td>
                                <button className="btn btn-primary"
                                    onClick={this.props.basket ? () => (this.orderOutBasket(index, this.props.setBasket)) : () => { }}>
                                    Delete
                                </button>
                            </td>
                        </tr>
                    )}

                </tbody>
            </table>
        </>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading......</em></p>
            : this.renderDefaultSepsTable(this.props.basket);

        return (
            <div>
                <h1 id="tabelLabel" >Cart</h1>
                <p>Count sepulks: {this.props.basket.length} <br></br> Total price: {this.props.basket.reduce(
                        (accumulator, currentValue) => accumulator + this.getPrice(currentValue),
                        0,
                    )}</p>
                <button className="btn btn-primary"
                    onClick={() => this.orderBasket(this.props.basket, this.props.user.token, this.props.setBasket)}>
                    Order
                </button>
                <br></br>
                <p>
                    {contents}
                </p>
            </div>
        );
    }

    async getDefaultSeps() {
        const response = await fetch('sep/default');
        const data = (await response.json()).map(sep =>
            sep.sex == 0 ?
                { ...sep, sex: "Male" }
                : { ...sep, sex: "Female" })
        this.setState({ forecasts: data, loading: false });
    }

    async orderOutBasket(index, setBasket) {
        const b = this.props.basket
        b.splice(index, 1)
        setBasket(b)
    }

    async orderBasket(basket, token, setBasket) {
        basket.forEach((sep) => this.postOrder(sep, token));
        setBasket([])
    }

    async postOrder(details, token) {
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
