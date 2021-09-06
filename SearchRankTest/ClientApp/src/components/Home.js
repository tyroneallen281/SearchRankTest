import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;
    constructor(props) {
        super(props);
        this.state = { searchTerms: "land registry search", domain: "www.infotrack.co.uk", data: [], loading: true };

    }
    handleSearchTermsChange = (e) => {
        this.setState({ searchTerms: e.target.value });
    };
    handleDomainChange = (e) => {
        this.setState({ domain: e.target.value });
    };
    proccessRequest() {
        const requestOptions = {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            
        };
        console.log(requestOptions);
        fetch('https://localhost:44384/api/SearchRank/GetRanks?searchTerms=' + this.state.searchTerms + '&domain=' + this.state.domain , requestOptions)
            .then(response => response.json())
            .then(data => {
                console.log(data);
                this.setState({ data: data, loading: false });
            });
    }

    static renderResults(data) {
        return (
            <div>
                {data.map(item =>
                    <div>
                        <hr />
                        <h4>{item.searchProviderName}</h4>
                       
                        {item.ranks.map(rank =>
                            <div>
                                <b>{rank.term}</b> <small>Rank:{rank.rank}</small>
                            </div>
                        )}
                    </div>
                )}
            </div>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderResults(this.state.data);

       
    return (
        <div>
            <h2>Search Rank Check</h2>
            <label>Domain</label>
            <input type="text" value={this.state.domain} onChange={this.handleDomainChange}/>
            <label>Search Terms<small>( , seperated)</small></label>
            <input type="text" value={this.state.searchTerms} onChange={this.handleSearchTermsChange}/>
            <br />
            <button onClick={() => this.proccessRequest()}>Process</button>
            <br/><hr />
            <h3>Search Rank Result</h3>
            {contents}
      </div>
    );
  }
}
