pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/sdk:7.0'
            label 'docker'
        }
    }
    
    environment {
        DOCKER_IMAGE = 'weather'
        DOCKER_REGISTRY = 'https://hub.docker.com/u/lapphan' // e.g., Docker Hub or Azure Container Registry
        DOCKER_CREDENTIALS_ID = 'lapphan' // Jenkins credentials ID for Docker login
    }


    stages {
        stage('Checkout') {
            steps {
                // Clone the repository
                git 'https://github.com/havefun02/Ssr_OpenWeatherApi.git'
            }
        }

        stage('Build') {
            steps {
                // Build the Docker image
                script {
                    sh 'docker build -t $DOCKER_IMAGE .'
                }
            }
        }

        stage('Test') {
            steps {
                // Run your tests here
                script {
                    // Example of running tests
                    sh 'dotnet test WeatherThirdParty.Tests/WeatherThirdParty.Tests.csproj'
                }
            }
        }

        stage('Publish') {
            steps {
                // Publish the Docker image to a registry
                script {
                    // Log in to Docker registry
                    docker.withRegistry("https://$DOCKER_REGISTRY", "$DOCKER_CREDENTIALS_ID") {
                        sh 'docker push $DOCKER_IMAGE'
                    }
                }
            }
        }

        /*stage('Deploy') {
            steps {
                // Deploy the application (e.g., using SSH, Kubernetes, etc.)
                script {
                    // Example SSH deployment
                    sh '''
                    ssh user@yourserver "docker pull $DOCKER_IMAGE && docker stop yourcontainername || true && docker rm yourcontainername || true && docker run -d -p 80:80 --name yourcontainername $DOCKER_IMAGE"
                    '''
                }
            }
        }*/
    }

    post {
        always {
            // Clean up Docker images to save space
            sh 'docker rmi $DOCKER_IMAGE || true'
        }
    }
}
