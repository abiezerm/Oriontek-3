import Companies from './Componentes/companies';
function App() {
  return (
    <div className="App">
      <Companies></Companies>
    </div>
  );
}

// async function getCompanies() {
//   const reposne = await fetch('http://localhost:5000/graphql/', {
//     method: 'POST',
//     headers: {
//       'content-type': 'application/json',
//       credentials: 'include',
//     },
//     body: JSON.stringify({
//       query: `
//     query{
//       companies
//       {
//         id
//         name
//         address
//         employees{
//           id
//           name
//           address
//         }
//       }
//     }`,
//     }),
//   });
//   const json = await reposne.json();
//   console.log(json.data.companies);
//   return json;
// }

export default App;
