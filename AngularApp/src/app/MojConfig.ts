import {AutentifikacijaToken} from "./helper/login-informacije";
import {AutentifikacijaHelper} from "./helper/autentifikacija-helper";

export class MojConfig{

  static adresa_servera="https://localhost:7296";

  static http_opcije= function (){

    let autentifikacijaToken = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken;
    let mojtoken = "";

    if (autentifikacijaToken!=null)
      mojtoken = autentifikacijaToken.vrijednost;
    return {
      headers: {
        'autentifikacija-token': mojtoken,
      }
    };
  }
}
