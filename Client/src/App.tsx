import './App.css'
import {BrowserRouter, Route, Routes} from "react-router-dom";
import {EditWords} from "./Pages/EditWords/EditWords";

function App() {


  return (
      <BrowserRouter>
          <div className="container">
              <Routes>
                <Route path='/words' element={<EditWords/>}/>
              </Routes>
          </div>

      </BrowserRouter>
  )
}

export default App
