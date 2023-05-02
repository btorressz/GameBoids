import SceneKit

class WaterBoid: SCNNode {
    let maxSpeed: CGFloat = 5.0
    let maxSteerForce: CGFloat = 0.1
    let perceptionRadius: CGFloat = 10.0

    func update(_ currentTime: TimeInterval) {
        // Calculate the average position and velocity of all nearby water boids
        var averagePosition = SCNVector3.zero
        var averageVelocity = SCNVector3.zero
        var boidCount = 0
        for boid in self.parent!.childNodes {
            if boid != self && self.position.distance(to: boid.position) < perceptionRadius {
                averagePosition += boid.position
                averageVelocity += boid.physicsBody!.velocity
                boidCount += 1
            }
        }
        if boidCount > 0 {
            averagePosition /= Float(boidCount)
            averageVelocity /= Float(boidCount)
        }

        // Calculate the desired velocity
        let desiredVelocity = averagePosition - self.position + averageVelocity

        // Steer towards the desired velocity
        var steerForce = desiredVelocity - self.physicsBody!.velocity
        steerForce.limit(maxSteerForce)
        self.physicsBody?.applyForce(steerForce, asImpulse: true)

        // Limit the speed
        self.physicsBody?.velocity.limit(maxSpeed)
    }
}
