import { Component } from '@angular/core';

@Component({
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent {
    datosPersonales = {
        name: 'Fernando José  Flores Mendoza',
        description: `Soy un Desarrollador Full Stack con 3 años de experiencia en la construcción de aplicaciones. Mi enfoque
        abarca tanto el desarrollo del frontend como del backend, permitiéndome trabajar de manera integral en
        todos los aspectos de un proyecto. Reconocido por mi habilidad para colaborar efectivamente en equipos,
        me destaco en la coordinación para lograr los objetivos del proyecto. Mi creatividad y dedicación para
        abordar problemas complejos me permiten aportar ideas innovadoras. Estoy entusiasmado por seguir
        creciendo como profesional en el desarrollo web`,
        linkedin:
            'https://www.linkedin.com/public-profile/settings?trk=d_flagship3_profile_self_view_public_profile',
        correo: 'fernandojose28032002@gmail.com',
        telefono: '+505 885-0074',
    };
}
