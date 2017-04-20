namespace ManyToMany.Controllers {

    export class HomeController {
        public message = 'Hello from the home page!';
        public movies;

        constructor(private movieService: ManyToMany.Services.MovieService) {
            this.movies = movieService.listMovies();
        }
    }


    export class DetailsController {
        public movie;
        public actor;

        public addActor() {
            this.movieService.addActor(this.movie.id, this.actor).then(() => this.$state.go('home')
            );
            console.log(this.actor);
        }

        constructor(private movieService: ManyToMany.Services.MovieService, private $state: ng.ui.IStateService, private $stateParams: ng.ui.IStateParamsService) {
            this.movie = movieService.getMovie($stateParams['id']);
        }
    }


    export class SecretController {
        public secrets;

        constructor($http: ng.IHttpService) {
            $http.get('/api/secrets').then((results) => {
                this.secrets = results.data;
            });
        }
    }


    export class AboutController {
        public message = 'Hello from the about page!';
    }

}
