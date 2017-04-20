namespace ManyToMany.Services {

    export class MovieService {

        private MovieResource;

        public listMovies() {
            return this.MovieResource.query();
        }

        public addActor(movieId, actor) {
            return this.MovieResource.save({ id: movieId }, actor).$promise;
        }

        public getMovie(id) {
            return this.MovieResource.get({ id: id });
        }
        
        constructor($resource: angular.resource.IResourceService) {
            this.MovieResource = $resource('/api/movies/:id');
        }

    }

    angular.module("ManyToMany").service("movieService", MovieService);
}
