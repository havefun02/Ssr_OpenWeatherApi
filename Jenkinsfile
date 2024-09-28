pipeline {
    agent any
    
    environment {
        IMAGE_NAME="weather_app"
        IMAGE_TAG = 'latest'
        DOCKER_REGISTRY = 'hub.docker.com/u/' // e.g., Docker Hub or Azure Container Registry
        DOCKER_CREDENTIALS_ID = 'lapphan' // Jenkins credentials ID for Docker login
    }


    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

         stage('Build Docker Image') {
            steps {
                script {
                    echo "Start build image"
                    echo "Add condition for build image statement"
                    echo "Add condition for build image statement"
                    echo "Add condition for build image statement"

                    if (isUnix()) {
                       sh "docker build -t ${IMAGE_NAME}:${IMAGE_TAG} ."
                    } else {
                        bat "docker build -t ${IMAGE_NAME}:${IMAGE_TAG} ."
                    }
                }
            }
        }

        stage('Run Tests in Docker') {
            steps {
                script {
                    echo "Run test in docker"

                    // Run the tests inside a Docker container
                    // The .NET test project is executed with `dotnet test`
                    sh "docker run --rm ${IMAGE_NAME}:${IMAGE_TAG} dotnet test --no-build --verbosity normal"
                }
            }
        }
        stage('Push Docker Image to Registry') {
            steps {
                script {
                    // Push the built image to a Docker registry if needed
                    sh "docker login -u 'your-username' -p 'your-password'"
                    sh "docker tag ${IMAGE_NAME}:${IMAGE_TAG} your-docker-repo/${IMAGE_NAME}:${IMAGE_TAG}"
                    sh "docker push your-docker-repo/${IMAGE_NAME}:${IMAGE_TAG}"
                }
            }
        }

        stage('Deploy') {
            steps {
                // Deploy the application (e.g., using SSH, Kubernetes, etc.)
                script {
                    // Example SSH deployment
                    sh '''
                    ssh user@yourserver "docker pull $DOCKER_IMAGE && docker stop yourcontainername || true && docker rm yourcontainername || true && docker run -d -p 80:80 --name yourcontainername $DOCKER_IMAGE"
                    '''
                }
            }
        }
    }

    post {
        always {
            // Always remove the Docker image locally after the pipeline finishes
            sh "docker rmi ${IMAGE_NAME}:${IMAGE_TAG} || true"
        }
        success {
            echo "Pipeline completed successfully!"
        }
        failure {
            echo "Pipeline failed!"
        }
    }
}
