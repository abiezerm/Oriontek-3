import React from 'react';

export default class Companies extends React.Component {
  state = {
    loading: true,
    company: [],
    newCompanyName: '',
    newCompanyAddress: '',
    newEmployees: [],
  };

  async componentDidMount() {
    this.loadCompanies();
  }

  loadCompanies = async () => {
    const url = 'http://localhost:5000/graphql/';

    const response = await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        credentials: 'include',
      },
      body: JSON.stringify({
        query: `
    query{
      companies
      {
        id
        name
        address
        employees{
          id
          name
          address
        }
      }
    }`,
      }),
    });
    const data = await response.json();
    this.setState({ company: data.data.companies, loading: false });
  };

  deleteCompanyClicked = async (id) => {
    const url = 'http://localhost:5000/graphql/';

    await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        credentials: 'include',
      },
      body: JSON.stringify({
        query: `
    mutation{
      deleteCompany(id: ${id})
      {
        id
      }
    }`,
      }),
    });
    this.loadCompanies();
  };

  createCompanyClicked = async (name, address) => {
    const url = 'http://localhost:5000/graphql/';

    await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        credentials: 'include',
      },
      body: JSON.stringify({
        query: `
    mutation{
      addCompany(input: {name: "${name}", address: "${address}", employees: []})
      {
        id
      }
    }`,
      }),
    });
    this.loadCompanies();
  };
  deleteEmployeeClicked = async (id) => {
    const url = 'http://localhost:5000/graphql/';

    await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        credentials: 'include',
      },
      body: JSON.stringify({
        query: `
    mutation{
      deleteEmployee(id: ${id})
      {
        id
      }
    }`,
      }),
    });
    this.loadCompanies();
  };
  addEmployeeToState = (index) => {
    this.state.newEmployees.push({ name: '', address: '' });
  };
  handleChangeNewEmployeeName = (e, index) => {
    let currentEmployees = [...this.state.newEmployees];
    currentEmployees[index].name = e.target.value;
    this.setState({ newEmployees: currentEmployees });
  };
  handleChangeNewEmployeeAdress = (e, index) => {
    let currentEmployees = [...this.state.newEmployees];
    currentEmployees[index].address = e.target.value;
    this.setState({ newEmployees: currentEmployees });
  };
  createNewEmployeeClicked = async (compId, index) => {
    const url = 'http://localhost:5000/graphql/';

    await fetch(url, {
      method: 'POST',
      headers: {
        'content-type': 'application/json',
        credentials: 'include',
      },
      body: JSON.stringify({
        query: `
    mutation{
      addEmployee(input: {companyId: ${compId}, name: "${this.state.newEmployees[index].name}",address: ["${this.state.newEmployees[index].address}"]})
      {
        id
      }
    }`,
      }),
    });
    this.loadCompanies();
  };
  render() {
    if (this.state.loading) {
      return <div>loading...</div>;
    }
    let companyJsx = this.state.company.map((company, index) => (
      <div key={index}>
        <div>
          {company.name}
          {company.address}
          {this.addEmployeeToState(index)}
          <button onClick={(e) => this.deleteCompanyClicked(company.id)}>
            Delete Company
          </button>
          <input
            type="text"
            value={this.state.newEmployees[index].name}
            onChange={(e) => this.handleChangeNewEmployeeName(e, index)}
          ></input>
          <input
            type="text"
            value={this.state.newEmployees[index].address}
            onChange={(e) => this.handleChangeNewEmployeeAdress(e, index)}
          ></input>
          <button
            onClick={(e) => this.createNewEmployeeClicked(company.id, index)}
          >
            Add employees
          </button>
        </div>
        <div>
          {company.employees.map((emp, index) => (
            <div key={index}>
              <div>
                {emp.name}
                <button onClick={(e) => this.deleteEmployeeClicked(emp.id)}>
                  Delete Employee
                </button>
              </div>
              <input type="text"></input>
              <button>Add Address</button>
              {emp.address.map((add, index) => (
                <div key={index}>{add}</div>
              ))}
            </div>
          ))}
        </div>
      </div>
    ));
    return (
      <div>
        {companyJsx}
        <input
          type="text"
          value={this.state.newCompanyName}
          onChange={(e) => {
            return this.setState({ newCompanyName: e.target.value });
          }}
        ></input>
        <input
          type="text"
          value={this.state.newCompanyAddress}
          onChange={(e) => {
            return this.setState({ newCompanyAddress: e.target.value });
          }}
        ></input>
        <button
          onClick={(e) =>
            this.createCompanyClicked(
              this.state.newCompanyName,
              this.state.newCompanyAddress
            )
          }
        >
          Add Company
        </button>
      </div>
    );
  }
}
