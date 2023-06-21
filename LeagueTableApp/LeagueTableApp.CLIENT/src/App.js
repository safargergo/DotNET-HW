import React, { Component } from 'react';
//import { Home } from "./Home";
//import { Leagues } from "./Leagues";
//import { Teams } from "./Teams";
//import { Matches } from "./Matches";
//import { Button } from 'bootstrap';

export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = "login";//{ leagues: [], loading: true };
    }

    componentDidMount() {
        this.populateLeagueData();
    }

    static renderLeagueTable(leagues) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>League</th>
                    </tr>
                </thead>
                <tbody>
                    {leagues.map(league =>
                        <tr key={league.id}>
                            <td>{league.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    /*static top() {
        return (
            
        );
    }

    static login() {
        return (
            <div>

            </div>
        );
    }*/

    render() {
        /*let contents = this.state.loading
            ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
            : App.renderLeagueTable(this.state.leagues);
            */


        if (this.state === "login") {
            return (
                /*this.top(),
                this.login(),
                <p>Valami </p>*/
                <div>
                    <h1 className="d-flex justify-content-center m-4" >League Table App</h1>
                    <h6 className="d-flex justify-content-center m-5">This is an app for managing legues with teams and for generate matches.</h6>

                    <div class="row">
                        <div class="col-lg-6" className='d-flex justify-content-center'>
                            <a className='btn btn-primary' href='/home'>Click here to login</a>
                            <button onClick={() => (this.setState(""))}>Masik login</button>
                        </div>
                    </div>
                </div>
            );
        } else {
            return (
                <div>
                    <h1 className="d-flex justify-content-center m-4" >League Table App</h1>
                    <h6 className="d-flex justify-content-center m-5">This is an app for managing legues with teams and for generate matches.</h6>
                </div>
            );
        }
    }

    async belepes(){
        this.setState("");
    }

    async populateLeagueData() {
        const response = await fetch('leaguetable');
        const data = await response.json();
        this.setState({ leagues: data, loading: false });
    }
}
