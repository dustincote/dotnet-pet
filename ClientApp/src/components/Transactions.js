import React, { Component } from 'react';
import { connect } from 'react-redux';
import axios from 'axios';
import './Transactions.css';


class Transactions extends Component {

    state = {
        page: 0
    }
    componentDidMount = async () => {
        const response = await axios.get(`api/transactions/${this.state.page}/10`);
        this.props.dispatch({ type: 'SET_TRANSACTIONS', payload: response.data });
    }
    componentDidUpdate = async () => {
        const response = await axios.get(`api/transactions/${this.state.page}/10`);
        this.props.dispatch({ type: 'SET_TRANSACTIONS', payload: response.data });
    }

    nextPage = () => {
        this.setState({
            page: this.state.page + 10
        });
    }

    previousPage = () => {
        if(this.state.page > 0){
        this.setState({
            page: this.state.page - 10
        });}

    }

    render() {

        return (
            <>
                <h2> Transaction History </h2>
                <div className="table-responsive">
                    <div className="historyButtons">
                    <button className={"form-control btn btn-primary mr-2 col-md-2"} onClick={this.previousPage}>Previous Page</button>
                    <button className={"form-control btn btn-primary col-md-2"} onClick={this.nextPage}>Next Page</button>
                    </div>
                    <table className='table table-striped table-bordered table-hover' aria-labelledby="tabelLabel">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Transaction</th>
                                <th>Time</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.transactions.map((transaction) => {
                                return (
                                    <tr key={transaction.id}>
                                        <td>{transaction.id}</td>
                                        <td>{transaction.transaction}</td>
                                        <td>{transaction.transactionTime}</td>
                                    </tr>
                                )
                            }
                            )}
                        </tbody>
                    </table>

                </div>
            </>
        );
    }

}
const mapStateToProps = (state) => ({ transactions: state.transactions });
export default connect(mapStateToProps)(Transactions);