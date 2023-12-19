import React, { Component } from 'react';

export class SepsOrder extends Component {
    static displayName = SepsOrder.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], size: "", adress: "", sex: "Male", dropdown: false, loading: true };
        this.renderDefaultSepsTable = this.renderDefaultSepsTable.bind(this);
        this.changeAdress = this.changeAdress.bind(this);
        this.changeSize = this.changeSize.bind(this);
        this.orderInBasket = this.orderInBasket.bind(this);
        this.getPrice = this.getPrice.bind(this);
        this.changeSex = this.changeSex.bind(this);
        this.toggleOpen = this.toggleOpen.bind(this);
    }


    changeAdress(event) {
        this.setState({ adress: event.target.value });
    }

    changeSex(value) {
        this.setState({ sex: value });
    }

    toggleOpen() {
        this.setState({ dropdown: !this.state.dropdown });
    }

    changeSize(event) {
        if (!isNaN(Number(event.target.value)))
            this.setState({ size: event.target.value });
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
            <div className="row">
                {
                    seps.map(sep => 
                        <div key={sep.id} className="col-sm-4 pt-3">
                            <div className="card">
                                <div className="card-body">
                                    <h5 className="card-title">{sep.sex}</h5>
                                    <p className="card-text">Size: {sep.size}</p>
                                    <button className=
                                        {this.props.user && this.props.user.userType == "Customer" ?
                                            "btn btn-primary" : "btn btn-primary disabled"}
                                        onClick={this.props.user ?
                                            () => (this.orderInBasket(sep.size, sep.sex, this.props.setBasket)) : () => { }}>
                                        Add to cart
                                    </button>
                                </div>
                            </div>
                        </div>
                    )}
                <div className="col-sm-4 pt-3">
                    <div className="card">
                        <div className="card-body">
                            <h5 className="card-title">
                                <div type="button" className="dropdown">
                                    <button type="button" className={this.props.user && this.props.user.userType == "Customer" ?
                                        "btn btn-primary" : "btn btn-primary disabled"} onClick={this.toggleOpen}>
                                        {this.state.sex}
                                    </button>
                                    <div
                                        className={`dropdown-menu ${this.state.dropdown ? 'show' : ''}`}
                                        aria-labelledby="dropdownMenuButton">
                                        <button class="dropdown-item" onClick={() => { this.toggleOpen(); this.changeSex("Male") }}>Male</button>
                                        <button class="dropdown-item" onClick={() => { this.toggleOpen(); this.changeSex("Female") }}>Female</button>
                                    </div>
                                </div>
                            </h5>
                            <p className="card-text">
                                <input className="form-control" type="text" aria-describedby="validationServer" placeholder="Size"
                                    value={this.state.size} onChange={this.changeSize} />
                            </p>
                            <button className=
                                {this.props.user && this.props.user.userType == "Customer" ?
                                    "btn btn-primary" : "btn btn-primary disabled"}
                                onClick={this.props.user ?
                                    () => (this.orderInBasket(this.state.size, this.state.sex, this.props.setBasket)) : () => { }}>
                                Add to cart
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading......</em></p>
            : this.renderDefaultSepsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Order sepulk</h1>
                <p>Choose default or custom.</p>
                <input className="form-control pb-3" type="text" aria-describedby="validationServer" placeholder="Adress"
                    value={this.state.adress} onChange={this.changeAdress} />
                {contents}
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

    async orderInBasket(size, sex, setBasket) {
        var details =
        {
            Size: String(size),
            Sex: sex,
            deliveryAdress: this.state.adress
        }
        setBasket([...this.props.basket, details])
    }

    async postOrder(size, sex, token) {
        var details =
        {
            Size: String(size),
            Sex: sex,
            deliveryAdress: this.state.adress
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
